using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace PollEditorBot.Commands.TwoSteps;

public class ChangePollQuestionByTemplateBotCommand : TwoStepsPollBotCommand
{
    public ChangePollQuestionByTemplateBotCommand(Poll poll) : base(poll) { }

    protected override void ExecuteFirstStep()
        => MessageStr = "Now send your question template where <code>{0}</code> contains your main question";

    protected override void ExecuteSecondStep(string commandStr)
    {
        if (commandStr.Contains("{0}"))
            Poll = pollEditor.ChangePollQuestionByTemplate(commandStr);
    }
}