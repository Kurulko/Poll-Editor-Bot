using PollEditorBot.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace PollEditorBot.Commands.TwoSteps;

public class DeletePollOptionBotCommand : ChangeQuizBotCommand
{
    public DeletePollOptionBotCommand(Poll poll) : base(poll) { }

    int CountOfOptions => PollHelper.CountOfOptions(Poll);
    bool IsQuiz => PollHelper.IsQuiz(Poll);
    int? CorrectOptionId => Poll.CorrectOptionId;
    protected override void ExecuteQuizFirstStep()
            => MessageStr = $"Send the option number from 1 to {CountOfOptions} which you want to delete." + (IsQuiz ? $" Except the number {CorrectOptionId + 1}" : string.Empty);

    protected override void ExecuteSecondStep(string commandStr)
    {
        if (!int.TryParse(commandStr, out int optionNumber) || optionNumber <= 0 || optionNumber > CountOfOptions || (IsQuiz && optionNumber == CorrectOptionId))
            throw PollEditorException.IncorrectOptionNumber(CountOfOptions);

        Poll = pollEditor.DeletePollOption(optionNumber - 1);
    }
}