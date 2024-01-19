using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace PollEditorBot.Commands.TwoSteps;

public abstract class TwoStepsPollBotCommand : PollBotCommand
{
    public TwoStepsPollBotCommand(Poll poll) : base(poll) { }

    public override void Execute(string? commandStr)
    {
        if (IsFinished ?? true)
        {
            ExecuteFirstStep();
            IsFinished = false;
        }
        else if(commandStr is not null)
        {
            ExecuteSecondStep(commandStr);
            ReplyMarkup = new ReplyKeyboardRemove();
            IsFinished = true;
        }
    }

    protected abstract void ExecuteFirstStep();
    protected abstract void ExecuteSecondStep(string commandStr);
}
