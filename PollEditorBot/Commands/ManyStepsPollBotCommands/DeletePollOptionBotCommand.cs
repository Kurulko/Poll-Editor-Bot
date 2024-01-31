using PollEditorBot.Commands.ManySteps;
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

namespace PollEditorBot.Commands.ManyStepsPollBotCommands;

public class DeletePollOptionBotCommand : ManyStepsPollBotCommand
{
    public DeletePollOptionBotCommand(Poll poll) : base(poll) { }

    int CountOfOptions => PollHelper.CountOfOptions(Poll);
    protected override void ExecuteFirstStep()
    {
        if (CountOfOptions <= TelegramSettings.MinPollCountOfOptions)
        {
            throw PollEditorException.MinCountOfOptionsReached();
        }
        else if (!PollHelper.IsQuiz(Poll))
        {
            throw PollEditorException.QuizRequiredToCommit();
        }
        else
        { 
            MessageStr = $"Send the option number from 1 to {CountOfOptions} which you want to delete.";
            IEnumerable<KeyboardButton> keyboardButtons = Enumerable.Range(1, CountOfOptions).Select(n => new KeyboardButton(n.ToString()));
            ReplyMarkup = new ReplyKeyboardMarkup(keyboardButtons) { ResizeKeyboard = true }; ;
        }
    }

    PollBotCommand? changeCorrectOption;
    protected override void ExecuteOtherSteps(string commandStr)
    {
        if (changeCorrectOption is not null)
        {
            changeCorrectOption.Execute(commandStr);
            CopyValues(changeCorrectOption);
        }
        else
        {
            if (int.TryParse(commandStr, out int optionNumber) && optionNumber > 0 && optionNumber <= CountOfOptions)
            {
                Poll = pollEditor.DeletePollOption(optionNumber - 1);

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
                throw PollEditorException.IncorrectOptionNumber(CountOfOptions);
            }

        }
    }
}