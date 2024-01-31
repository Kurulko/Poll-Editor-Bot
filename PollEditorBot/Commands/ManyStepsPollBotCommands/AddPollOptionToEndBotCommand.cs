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

public class AddPollOptionToEndBotCommand : ManyStepsPollBotCommand
{
    public AddPollOptionToEndBotCommand(Poll poll) : base(poll) { }

    protected override void ExecuteFirstStep()
    {
        if (PollHelper.CountOfOptions(Poll) >= TelegramSettings.MaxPollCountOfOptions)
        {
            throw PollEditorException.MaxCountOfOptionsReached();
        }
        else
        {
            MessageStr = $"Send a poll option which you want to add to the end of the list {PollRequirementsStr.PollOptionRequirementsHTMLStr}.";
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
            int length = commandStr.Length;
            if (length >= TelegramSettings.MinPollOptionLength && length <= TelegramSettings.MaxPollOptionLength)
            {
                Poll = pollEditor.AddPollOption(commandStr!);

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