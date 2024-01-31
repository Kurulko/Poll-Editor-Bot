using PollEditorBot.Exceptions;
using PollEditorBot.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace PollEditorBot.Commands.TwoSteps;

public abstract class ChangeQuizBotCommand : TwoStepsPollBotCommand
{
    public ChangeQuizBotCommand(Poll poll) : base(poll) { }

    protected abstract void ExecuteQuizFirstStep();

    protected override void ExecuteFirstStep()
    {
        if (PollHelper.IsQuiz(Poll))
            ExecuteQuizFirstStep();
        else
            throw PollEditorException.QuizRequiredToCommit();
    }

}