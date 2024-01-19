using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types;

namespace PollEditorBot.Commands;

public class HelpBotCommand : BaseBotCommand
{
    const string onlyForQuizzes = "only for quizzes";
    const string optional = "optional";
    const string ifPresent = "if present";
    string HTMLItalicInBrackets(params string[] strs)
        => $"<i>({string.Join(", ", strs)})</i>";
    public override void Execute(string? commandStr)
    {
        string resultStr = string.Empty;

        resultStr += "<b>To edit a poll, use these commands after sending a poll</b>:\n\n";

        resultStr += $"• {CommandsStr.ChangePollVisibility} - edit poll visibility (public/anonymous)\n";
        resultStr += $"• {CommandsStr.ChangePollQuestion} - edit poll question\n";
        resultStr += $"• {CommandsStr.ChangePollQuestionByTemplate} - edit poll question by template\n";
        resultStr += $"• {CommandsStr.ChangePollType} - edit poll type\n";
        resultStr += $"• {CommandsStr.ChangePollCorrectOption} - edit poll's correct option {HTMLItalicInBrackets(onlyForQuizzes)}\n";
        resultStr += $"• {CommandsStr.ChangePollExplanation} - edit poll explanation {HTMLItalicInBrackets(onlyForQuizzes, optional)}\n";
        resultStr += $"• {CommandsStr.DropPollExplanation} - remove poll explanation {HTMLItalicInBrackets(onlyForQuizzes, ifPresent, optional)}\n";
        resultStr += $"• {CommandsStr.ChangePollOpenPeriod} - edit poll's open period {HTMLItalicInBrackets(optional)}\n";
        resultStr += $"• {CommandsStr.DropPollOpenPeriod} - remove poll's open period {HTMLItalicInBrackets(optional, ifPresent)}\n";
        resultStr += $"• {CommandsStr.ChangePollOption} - edit a poll option\n";
        resultStr += $"• {CommandsStr.InsertPollOption} - insert a poll option\n";
        resultStr += $"• {CommandsStr.AddPollOptionToEnd} - add a poll option to end\n";
        resultStr += $"• {CommandsStr.DeletePollOption} - remove a poll option\n";
        resultStr += $"• {CommandsStr.ChangePollOptions} - edit all poll options\n";
        resultStr += $"• {CommandsStr.ChangePollIsMultipleAnswers} - edit - multiple answers\n";

        resultStr += "\nFor other commands that require a poll:\n\n";

        resultStr += $"• {CommandsStr.GetTextPoll} - get text poll\n";

        resultStr += "\n<b>No need to send a poll for these options</b>:\n\n";

        resultStr += $"• {CommandsStr.CreatePoll} - create a poll\n";

        resultStr += "\nOthers:\n\n";

        resultStr += $"• {CommandsStr.Start} - start the bot\n";
        resultStr += $"• {CommandsStr.Stop} - stop the bot\n";
        resultStr += $"• {CommandsStr.Help} - view all bot commands\n";

        resultStr += $"\n❗️<b>IMPORTANT:</b> The quiz must be submitted on your behalf and must be answered if it is not yours.\n";
        resultStr += $"⚠️<b>WARNING:</b> Once the quiz is edited, all the answers will be discarded.\n";

        MessageStr = resultStr;
        IsStrResponse = true;
        IsFinished = true;
    }

}
