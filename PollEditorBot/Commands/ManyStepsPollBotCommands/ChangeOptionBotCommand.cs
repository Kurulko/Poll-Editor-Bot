using PollEditorBot.Commands.TwoSteps;
using PollEditorBot.Exceptions;
using PollEditorBot.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace PollEditorBot.Commands.ManySteps;

public class ChangeOptionBotCommand : ManyStepsPollBotCommand
{
    public ChangeOptionBotCommand(Poll poll) : base(poll) { }

    int CountOfOptions => PollHelper.CountOfOptions(Poll);
    protected override void ExecuteFirstStep()
    {
        MessageStr = $"Send the option number from 1 to {CountOfOptions} which you want to edit.";
        IEnumerable<KeyboardButton> keyboardButtons = PollHelper.KeyboardNumberButtons(CountOfOptions);
        ReplyMarkup = new ReplyKeyboardMarkup(keyboardButtons) { ResizeKeyboard = true }; ;
    }

    int? chosenOptionNumber;
    protected override void ExecuteOtherSteps(string commandStr)
    {
        if (chosenOptionNumber is null)
        {
            if (int.TryParse(commandStr, out int optionNumber))
            {
                if (optionNumber > 0 && optionNumber <= CountOfOptions)
                {
                    chosenOptionNumber = optionNumber;
                    MessageStr = $"Please send a poll option {PollRequirementsStr.PollOptionRequirementsHTMLStr}.";

                    ReplyMarkup = new ReplyKeyboardRemove();
                    IsStrResponse = true;
                }
                else
                {
                    throw PollEditorException.IncorrectOptionNumber(CountOfOptions);
                }
            }
            else
                throw PollEditorException.TypeNumberRequired();
        }
        else
        {
            int length = commandStr.Length;
            if (length >= TelegramSettings.MinPollOptionLength && length <= TelegramSettings.MaxPollOptionLength)
            {
                Poll = pollEditor.ChangePollOption(commandStr!, chosenOptionNumber.Value - 1);
                IsStrResponse = false;
                IsFinished = true;
            }
            else
            {
                throw PollEditorException.InccorectLength(TelegramSettings.MinPollOptionLength, TelegramSettings.MaxPollOptionLength);
            }
        }
    }
}