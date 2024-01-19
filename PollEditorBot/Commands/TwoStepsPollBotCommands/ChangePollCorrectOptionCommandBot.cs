using PollEditorBot.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace PollEditorBot.Commands.TwoSteps;

public class ChangePollCorrectOptionCommandBot : ChangeQuizBotCommand
{
    public ChangePollCorrectOptionCommandBot(Poll poll) : base(poll) { }

    int CountOfOptions => PollHelper.CountOfOptions(Poll);
    protected override void ExecuteQuizFirstStep()
    {
        MessageStr = $"Send the correct option number from 1 to {CountOfOptions}";
        IEnumerable<KeyboardButton> keyboardButtons = Enumerable.Range(1, CountOfOptions).Select(n => new KeyboardButton(n.ToString()));
        ReplyMarkup = new ReplyKeyboardMarkup(keyboardButtons) { ResizeKeyboard = true }; ;
    }

    protected override void ExecuteSecondStep(string commandStr)
    {
        if (int.TryParse(commandStr, out int correctOptionNumber))
        {
            if (correctOptionNumber > 0 && correctOptionNumber <= CountOfOptions)
            {
                ReplyMarkup = new ReplyKeyboardRemove();
                Poll = pollEditor.ChangePollCorrectOptionId(correctOptionNumber - 1);
            }
            else
            {
                throw PollEditorException.IncorrectOptionNumber(CountOfOptions);
            }
        }
        else
            throw PollEditorException.TypeNumberRequired();
    }
}