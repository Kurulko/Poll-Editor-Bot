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

    public void LogCommandStrMessage(string commandStr)
        => LogMessage(() =>
        {
            logger.LogInformationLine($"Command: {commandStr}");
        });

    public void LogChatMessage(Chat chat)
        => LogMessage(() =>
        {
            logger.LogInformationLine($"Chat Id: {chat.Id}\n");

            ChatType chatType = chat.Type;
            logger.LogDebugLine($"Type: {chat.Type}");

            if (chatType == ChatType.Private)
            {
                if (chat.Username is { })
                    logger.LogDebugLine($"Username: @{chat.Username}");

                if (chat.FirstName is { })
                    logger.LogDebugLine($"FirstName: {chat.FirstName}");

                if (chat.LastName is { })
                    logger.LogDebugLine($"LastName: {chat.LastName}");

                if (chat.Bio is { })
                    logger.LogDebugLine($"Bio: {chat.Bio}");
            }
            else
            {
                if (chat.InviteLink is { })
                    logger.LogDebugLine($"InviteLink: {chat.InviteLink}");

                if (chat.Title is { })
                    logger.LogDebugLine($"Title: {chat.Title}");

                if (chat.Description is { })
                    logger.LogDebugLine($"Description: {chat.Description}");
            }
        });

    public void LogPollMessage(Poll poll)
        => LogMessage(() =>
        {
            logger.LogInformationLine($"Poll Id: {poll.Id}\n");
            logger.LogDebugLine((poll.IsAnonymous ? "Anonymous" : "Public") + " " + (poll.Type == "quiz" ? "quiz" : "poll"));
            logger.LogDebugLine($"Question: {poll.Question}\n");
            logger.LogDebugLine("Asnwers:");

            var options = poll.Options;
            for (int i = 0; i < options.Length; i++)
                logger.LogDebugLine($"\t {i + 1}) {options[i].Text} - {options[i].VoterCount} votes");

            logger.LogDebugLine("\n");

            if (poll.AllowsMultipleAnswers)
            {
                logger.LogDebugLine("Multiply Answers");
            }
            else if (poll.CorrectOptionId is { } correctOptionId)
            {
                PollOption correctOption = options[correctOptionId];
                logger.LogDebugLine($"Correct Option: {correctOptionId + 1} - {correctOption.Text} - {correctOption.VoterCount} votes");
                logger.LogDebugLine($"Explanations: {poll.Explanation}\n");
            }

            if (poll.OpenPeriod is { } openPeriod)
                logger.LogDebugLine($"Oper period: {openPeriod} seconds");

            logger.LogDebugLine($"Total votes: {poll.TotalVoterCount}");
        });

    void LogMessage(Action action)
    {
        DivideMessageStr();

        action();

        DivideMessageStr();
    }

    void DivideMessageStr()
        => logger.LogDebugLine(new string('-', 25));
}
