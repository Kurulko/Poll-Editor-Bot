using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace PollEditorBot.Commands.OneStep;

public class DropPollExplanationBotCommand : OneStepPollBotCommand
{
    public DropPollExplanationBotCommand(Poll poll) : base(poll) { }

    protected override void ExecuteOneStep()
        => Poll = pollEditor.ChangePollExplanation(null, null);
}
