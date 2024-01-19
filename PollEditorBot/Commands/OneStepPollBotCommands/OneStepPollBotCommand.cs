using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace PollEditorBot.Commands.OneStep;

public abstract class OneStepPollBotCommand : PollBotCommand
{
    public OneStepPollBotCommand(Poll poll) : base(poll) { }

    public override void Execute(string? commandStr)
    {
        ExecuteOneStep();
        IsFinished = true;
    }

    protected abstract void ExecuteOneStep();
}