using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;
using Telegram.Bot;
using PollEditorBot.Extensions;
using PollEditorBot.Settings;
using PollEditorBot.Exceptions;
using PollEditorBot.Loggers;
using PollEditorBot.Commands;
using Telegram.Bot.Types.ReplyMarkups;

namespace PollEditorBot;

public class PollEditorBotLauncher
{
    readonly ILogger logger;
    readonly Logging logging;
    readonly ITelegramBotClient bot;
    readonly MessageSender messageSender;
    public PollEditorBotLauncher(ILogger logger)
    {
        this.logger = logger;
        logging = new(logger);

        bot = TelegramSettings.CurrentBot();
        messageSender = new(bot);
    }

    readonly Dictionary<long, MessageReceiver> messageReceivers = new();
    string botName = string.Empty;
    public async Task StartReceivingAsync(CancellationTokenSource cts)
    {
        ReceiverOptions receiverOptions = new ReceiverOptions() { AllowedUpdates = Array.Empty<UpdateType>() };

        bot.StartReceiving(
            updateHandler: (ITelegramBotClient bot, Update update, CancellationToken _) => HandleUpdateAsync(update, cts),
            pollingErrorHandler: (ITelegramBotClient bot, Exception exc, CancellationToken _) => HandlePollingErrorAsync(exc, cts),
            receiverOptions,
            cts.Token);

        User me = await bot.GetMeAsync();
        botName = me.FirstName;
        logger.LogInformationLine(botName, $"\"{botName}\" started listening ...");
    }

    string GetSenderStr(Chat chat)
    {
        string senderStr = string.Empty;

        senderStr += chat.FirstName;

        if (chat.Username is string userName)
            senderStr += $" (@{userName})";

        return senderStr;
    }
    async Task HandleUpdateAsync(Update update, CancellationTokenSource cts)
    {
        if (update.Message is { } message)
        {
            Chat chat = message.Chat;

            int replyToMessageId = message.MessageId;
            long chatId = chat.Id;

            if (chat.Type == ChatType.Private)
            {
                string senderStr = GetSenderStr(chat);

                if (message.Text is { } messageText)
                    await HandleTextMessageAsync(senderStr, messageText, message.Entities, chatId, replyToMessageId, cts);
                else if (message.Poll is { } messagePoll)
                    await HandlePollMessageAsync(senderStr, messagePoll, chatId, replyToMessageId, cts);
                else
                    await HandleAnotherMessageTypeAsync(chatId, replyToMessageId, cts);
            }
            else
            {
                await LogWarningMessage(TelegramException.OnlyPrivateChatsSupported, chatId, replyToMessageId, null, cts);
            }
        }
    }

    async Task HandleTextMessageAsync(string sender, string messageText, IEnumerable<MessageEntity>? messageEntities, long chatId, int replyToMessageId, CancellationTokenSource cts)
    {
        try
        {
            logging.LogCommandStrMessage(sender, messageText);

            if (!messageReceivers.ContainsKey(chatId))
                messageReceivers.Add(chatId, new());

            MessageReceiver currentMessageReceiver = messageReceivers.GetValueOrDefault(chatId)!;

            if (currentMessageReceiver.BotCommand is not null)
            {
                if (!IsMessageEntitiesTypeSupported(messageEntities))
                {
                    await LogWarningMessage(TelegramException.MessageEntityTypeNotSupported, chatId, replyToMessageId, null, cts);
                    return;
                }

                currentMessageReceiver.BotCommand.MessageEntities = messageEntities;
            }

            await currentMessageReceiver.Execute(messageText);

            BaseBotCommand botCommand = currentMessageReceiver.BotCommand!;

            bool isFinished = botCommand.IsFinished ?? false;
            IReplyMarkup? replyMarkup = botCommand.ReplyMarkup;

            if (isFinished && !botCommand.IsStrResponse)
            {
                var poll = botCommand.Poll!;
                await logging.LogPollMessageAsync(poll);
                await messageSender.SendPollMessageAsync(poll, chatId, replyToMessageId, replyMarkup, cts);
            }
            else if (botCommand.MessageStr is { } messageStr)
            {
                await logging.LogStrMessage(messageStr);
                await messageSender.SendTextMessageAsync(messageStr, chatId, replyToMessageId, replyMarkup, cts);
            }
        }
        catch (PollEditorException botExc)
        {
            string messageExc = botExc.Message;
            logger.LogWarningLine(messageExc);
            await messageSender.SendTextMessageAsync(messageExc, chatId, replyToMessageId, null, cts);
        }
        catch (ApiRequestException ex)
        {
            int delay = ex.Parameters?.RetryAfter ?? 0;
            if (delay > 0)
            {
                string messageExc = TelegramException.TooManyRequests(delay);
                logger.LogWarningLine(messageExc);
                await messageSender.SendTextMessageAsync(messageExc, chatId, replyToMessageId, null, cts);
                Task.Delay(delay * 1000).Wait();
            }
        }
        catch (Exception exc)
        {
            logger.LogCriticalLine(exc.Message);
            //await StopBotAsync(cts);
        }
    }

    bool IsMessageEntitiesTypeSupported(IEnumerable<MessageEntity>? messageEntities)
        => messageEntities?.All(msgEntity => Enum.IsDefined(msgEntity.Type)) ?? true;

    async Task HandlePollMessageAsync(string sender, Poll pollMessage, long chatId, int replyToMessageId, CancellationTokenSource cts)
    {
        if (PollHelper.IfQuizSentCorrectly(pollMessage))
        {
            MessageReceiver newMessageReceiver = new(pollMessage);

            if (!messageReceivers.ContainsKey(chatId))
                messageReceivers.Add(chatId, newMessageReceiver);
            else
                messageReceivers[chatId] = newMessageReceiver;

            await messageSender.SendTextMessageAsync("Now, please send one of the available commands.", chatId, replyToMessageId, new ReplyKeyboardRemove(), cts);
            await logging.LogPollMessageAsync(pollMessage);
        }
        else
        {
            await LogWarningMessage(TelegramException.QuizSentIncorrectly, chatId, replyToMessageId, null, cts);
        }
    }

    async Task HandleAnotherMessageTypeAsync(long chatId, int replyToMessageId, CancellationTokenSource cts)
            => await LogWarningMessage(TelegramException.MessageTypeNotSuitable, chatId, replyToMessageId, null, cts);

    async Task LogWarningMessage(string warningStr, long chatId, int replyToMessageId, IReplyMarkup? replyMarkup, CancellationTokenSource cts)
    {
        logger.LogWarningLine(warningStr);
        await messageSender.SendTextMessageAsync(warningStr, chatId, replyToMessageId, null, cts);
    }

    Task HandlePollingErrorAsync(Exception exc, CancellationTokenSource cts)
    {
        if (exc is ApiRequestException ex)
        {
            int delay = ex.Parameters?.RetryAfter ?? 0;
            if (delay > 0)
            {
                logger.LogErrorLine(TelegramException.TooManyRequests(delay));
                Task.Delay(delay * 1000).Wait();
                return Task.CompletedTask;
            }
        }
        
        logger.LogError(exc.Message);

        return Task.CompletedTask;
        //await StopBotAsync(cts);
    }

    async Task StopBotAsync(CancellationTokenSource cts)
    {
        User me = await bot.GetMeAsync();
        string fName = me.FirstName;
        logger.LogDebugLine($"\"{fName}\" finished listening ...");
        cts.Cancel();
    }
}
