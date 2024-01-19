using PollEditorBot.Editors;
using PollEditorBot.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace PollEditorBot.Commands.OneStep;

public class DropPollOpenPeriodBotCommand : OneStepPollBotCommand
{
    public DropPollOpenPeriodBotCommand(Poll poll) : base(poll) { }

    protected override void ExecuteOneStep()
        => Poll = pollEditor.ChangePollOpenPeriod(null);
}
