using PollEditorBot.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace PollEditorBot.Loggers;

public class Logging
{
    readonly ILogger logger;
    public Logging(ILogger logger)
        => this.logger = logger;

    public void LogCommandStrMessage(string sender, string commandStr)
        => LogMessage(() => logger.LogInformationLine(sender, $"Command: {commandStr}"));

    public void LogStrMessage(string sender, string messageStr)
        => LogMessage(() => logger.LogInformationLine(sender, messageStr));
    public async Task LogStrMessage(string commandStr)
    {
        string currentBotName = (await TelegramSettings.GetCurrentBotName())!;
        LogStrMessage(currentBotName, commandStr);
    }

    public async Task LogChatMessageAsync(Chat chat)
        => await LogMessageAsync(async () =>
        {
            string resultStr = string.Empty;

            resultStr += $"\nChat Id: {chat.Id}";

            ChatType chatType = chat.Type;
            resultStr += $"\nType: {chatType}";

            if (chatType == ChatType.Private)
            {
                if (chat.Username is { })
                    resultStr += $"\nUsername: @{chat.Username}";

                if (chat.FirstName is { })
                    resultStr += $"\nFirstName: {chat.FirstName}";

                if (chat.LastName is { })
                    resultStr += $"\nLastName: {chat.LastName}";

                if (chat.Bio is { })
                    resultStr += $"\nBio: {chat.Bio}";
            }
            else
            {
                if (chat.InviteLink is { })
                    resultStr += $"\nInviteLink: {chat.InviteLink}";

                if (chat.Title is { })
                    resultStr += $"\nTitle: {chat.Title}";

                if (chat.Description is { })
                    resultStr += $"\nDescription: {chat.Description}";
            }

            string currentBotName = (await TelegramSettings.GetCurrentBotName())!;
            logger.LogInformation(currentBotName, resultStr + "\n");
        });

    public async Task LogPollMessageAsync(Poll poll)
        => await LogMessageAsync(async () =>
        {
            string resultStr = string.Empty;

            resultStr += $"Poll Id: {poll.Id}\n";
            resultStr += "\n" + (poll.IsAnonymous ? "Anonymous" : "Public") + " " + (poll.Type == "quiz" ? "quiz" : "poll");
            resultStr += $"\nQuestion: {poll.Question}";
            resultStr += "\n\nAsnwers:";

            var options = poll.Options;
            for (int i = 0; i < options.Length; i++)
                resultStr += $"\n\t {i + 1}) {options[i].Text} - {options[i].VoterCount} votes";

            resultStr += "\n";

            if (poll.AllowsMultipleAnswers)
            {
                resultStr += "\nMultiply Answers";
            }
            else if (poll.CorrectOptionId is { } correctOptionId)
            {
                PollOption correctOption = options[correctOptionId];
                resultStr += $"\nCorrect Option: {correctOptionId + 1} - {correctOption.Text} - {correctOption.VoterCount} votes";
                resultStr += $"\nExplanations: {poll.Explanation}";
            }

            if (poll.OpenPeriod is { } openPeriod)
                resultStr += $"\nOper period: {openPeriod} seconds";

            resultStr += $"\nTotal votes: {poll.TotalVoterCount}";

            string currentBotName = (await TelegramSettings.GetCurrentBotName())!;
            logger.LogInformationLine(currentBotName, resultStr + "\n");
        });

    void LogMessage(Action action)
    {
        DivideMessageStr();
        action();
    }
    async Task LogMessageAsync(Func<Task> actionAsync)
    {
        DivideMessageStr();
        await actionAsync();
    }

    void DivideMessageStr()
        => logger.LogDebugLine(new string ('-', 25));
}
