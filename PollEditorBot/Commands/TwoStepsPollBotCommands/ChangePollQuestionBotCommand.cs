using PollEditorBot.Enums;
using PollEditorBot.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace PollEditorBot.Commands.TwoSteps;

public class ChangePollQuestionBotCommand : TwoStepsPollBotCommand
{
    public ChangePollQuestionBotCommand(Poll poll) : base(poll) { }

    protected override void ExecuteFirstStep()
        => MessageStr = "Now send your question";

    protected override void ExecuteSecondStep(string commandStr)
        => Poll = pollEditor.ChangePollQuestion(commandStr!);
}
