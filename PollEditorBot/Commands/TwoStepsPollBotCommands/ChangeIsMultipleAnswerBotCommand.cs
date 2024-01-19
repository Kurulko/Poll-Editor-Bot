using PollEditorBot.Exceptions;
using PollEditorBot.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace PollEditorBot.Commands.TwoSteps;

public class ChangeIsMultipleAnswersBotCommand : TwoStepsPollBotCommand
{
    public ChangeIsMultipleAnswersBotCommand(Poll poll) : base(poll) { }
    
    protected override void ExecuteFirstStep()
    {
        MessageStr = $"Send either <code>{true}</code>(Multiple Answers) or <code>{false}</code>(Not Multiple Answers).";
        IEnumerable<KeyboardButton> keyboardButtons = new List<KeyboardButton>()
        {
            new(true.ToString()), new(false.ToString())
        };
        ReplyMarkup = new ReplyKeyboardMarkup(keyboardButtons) { ResizeKeyboard = true }; ;
    }

    protected override void ExecuteSecondStep(string commandStr)
    {
        if (bool.TryParse(commandStr, out bool isMultipleAnswers))
        {
            if (PollHelper.IsQuiz(Poll))
                PollHelper.ChangePollTypeIntoRegular(pollEditor);
            Poll = pollEditor.ChangePollIsMultipleAnswers(isMultipleAnswers);
        }
        else
            throw PollEditorException.TypeBooleanRequired();
    }
}