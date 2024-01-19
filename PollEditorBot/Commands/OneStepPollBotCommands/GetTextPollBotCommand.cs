using PollEditorBot.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace PollEditorBot.Commands.OneStep;

public class GetTextPollBotCommand : OneStepPollBotCommand
{
    public GetTextPollBotCommand(Poll poll) : base(poll) { }

    protected override void ExecuteOneStep()
    {
        string resultStr = string.Empty;

        resultStr += $"<b>Id</b>: <code>{Poll.Id}</code>\n";
        resultStr += $"<b>Type</b>: <code>{Poll.Type.ParseToPollType()}</code>\n";
        resultStr += "<b>Visibility</b>: <code>" + (Poll.IsAnonymous ? "Anonymous" : "Public") + "</code>\n";
        resultStr += $"<b>Is Multiple Answers</b>: <code>{Poll.AllowsMultipleAnswers}</code>\n";
        resultStr += $"<b>Question</b>:\n '<code>{Poll.Question}</code>'\n";
        resultStr += $"<b>Answers</b>:\n";
        
        var options = Poll.Options;
        for (int i = 0; i < options.Length; i++)
            resultStr += $"\t {i + 1}) '<code>{options[i].Text}</code>'\n";

        if (Poll.CorrectOptionId is { } correctOptionId)
        {
            resultStr += $"<b>Correct Option Number</b>: <code>{correctOptionId + 1}</code>\n";

            if (Poll.Explanation is { } explanation)
                resultStr += $"<b>Explanation</b>:\n '<code>{explanation}</code>'\n";
        }

        if (Poll.OpenPeriod is { } openPeriod)
            resultStr += $"<b>Oper period</b>: <code>{openPeriod}</code> seconds\n";

        MessageStr = resultStr;
        IsStrResponse = true;
    }
}
