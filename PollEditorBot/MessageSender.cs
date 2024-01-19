using PollEditorBot.Extensions;
using PollEditorBot.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace PollEditorBot;

public class MessageSender
{
    readonly ITelegramBotClient bot;
    public MessageSender(ITelegramBotClient bot)
        => this.bot = bot;

    const ParseMode parseMode = ParseMode.Html;

    public async Task SendTextMessageAsync(string messageStr, long chatId, int replyToMessageId, IReplyMarkup? replyMarkup, CancellationTokenSource cts)
    {
        int maxLength = TelegramSettings.MaxLengthOfMessage;
        var responsesStr = messageStr.DevideStrIfMoreMaxLength(maxLength).ToList();

        Message message = new();
        for (int i = 0; i < responsesStr.Count(); i++)
        {
            await bot.SendTextMessageAsync(chatId, responsesStr[i], 
                parseMode: parseMode, 
                replyToMessageId: i == 0 ? replyToMessageId : message.MessageId, 
                replyMarkup: replyMarkup, 
                cancellationToken: cts.Token);
        }
    }

    public async Task SendPollMessageAsync(Poll poll, long chatId, int replyToMessageId, IReplyMarkup? replyMarkup, CancellationTokenSource cts)
    {
        string[] options = poll.Options.Select(p => p.Text).ToArray();
        PollType pollType = poll.Type == PollType.Regular.ToLowerCaseString() ? PollType.Regular : PollType.Quiz;

        await bot.SendPollAsync(chatId, poll.Question, options, 
            isAnonymous: poll.IsAnonymous, 
            type: pollType,
            allowsMultipleAnswers: poll.AllowsMultipleAnswers,
            correctOptionId: poll.CorrectOptionId,
            explanation: poll.Explanation,
            explanationEntities: poll.ExplanationEntities,
            openPeriod: poll.OpenPeriod,
            closeDate: poll.CloseDate,
            isClosed: poll.IsClosed,
            replyToMessageId: replyToMessageId,
            replyMarkup: replyMarkup, 
            cancellationToken: cts.Token);
    }
}
