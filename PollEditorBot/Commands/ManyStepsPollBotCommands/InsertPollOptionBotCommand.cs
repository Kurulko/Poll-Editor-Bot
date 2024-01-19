using PollEditorBot.Commands.ManySteps;
using PollEditorBot.Editors;
using PollEditorBot.Exceptions;
using PollEditorBot.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types;
using PollEditorBot.Commands.TwoSteps;

namespace PollEditorBot.Commands.ManyStepsPollBotCommands;

public class InsertPollOptionBotCommand : ManyStepsPollBotCommand
{
    public InsertPollOptionBotCommand(Poll poll) : base(poll) { }

    int CountOfOptions => PollHelper.CountOfOptions(Poll);
    protected override void ExecuteFirstStep()
    {
        MessageStr = $"Send the option index from 1 to {CountOfOptions} where you want to insert the option.";
        IEnumerable<KeyboardButton> keyboardButtons = PollHelper.KeyboardNumberButtons(CountOfOptions);
        ReplyMarkup = new ReplyKeyboardMarkup(keyboardButtons) { ResizeKeyboard = true }; ;
    }

    int? chosenOptionNumber;
    PollBotCommand? changeCorrectOption;
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
        else if(changeCorrectOption is not null)
        {
            changeCorrectOption.Execute(commandStr);
            CopyValues(changeCorrectOption);
        }
        else
        {
            int length = commandStr.Length;
            if (length >= TelegramSettings.MinPollOptionLength && length <= TelegramSettings.MaxPollOptionLength)
            {
                Poll = pollEditor.InsertPollOption(commandStr!, chosenOptionNumber.Value - 1);

                if (PollHelper.IsQuiz(Poll))
                {
                    changeCorrectOption = new ChangePollCorrectOptionCommandBot(Poll);
                    changeCorrectOption.Execute(null);
                    CopyValues(changeCorrectOption);
                }
                else
                {
                    IsStrResponse = false;
                    IsFinished = true;
                }
            }
            else
            {
                throw PollEditorException.InccorectLength(TelegramSettings.MinPollOptionLength, TelegramSettings.MaxPollOptionLength);
            }
        }
    }
}