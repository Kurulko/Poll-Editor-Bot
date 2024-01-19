using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace PollEditorBot.Commands.ManySteps;

public abstract class ManyStepsPollBotCommand : PollBotCommand
{
    public ManyStepsPollBotCommand(Poll poll) : base(poll) { }

    public override void Execute(string? commandStr)
    {
        if (IsFinished ?? true)
        {
            ExecuteFirstStep();
            IsFinished = false;
        }
        else if (commandStr is not null)
        {
            ExecuteOtherSteps(commandStr);
        }
    }

    protected abstract void ExecuteFirstStep();
    protected abstract void ExecuteOtherSteps(string commandStr);
}
