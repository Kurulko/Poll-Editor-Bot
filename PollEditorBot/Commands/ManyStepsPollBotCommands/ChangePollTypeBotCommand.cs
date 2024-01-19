using Microsoft.VisualBasic;
using PollEditorBot.Commands.TwoSteps;
using PollEditorBot.Extensions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace PollEditorBot.Commands.ManySteps;

public class ChangePollTypeBotCommand : ManyStepsPollBotCommand
{
    public ChangePollTypeBotCommand(Poll poll) : base(poll) { }

    protected override void ExecuteFirstStep()
    {
        MessageStr = "Send one of poll types: either <code>Quiz</code> or <code>Regular</code>(usual poll)";
        IEnumerable<KeyboardButton> keyboardButtons = new List<KeyboardButton>()
        {
            new("Quiz"), new("Regular")
        };
        ReplyMarkup = new ReplyKeyboardMarkup(keyboardButtons) { ResizeKeyboard = true }; ;
    }

    ChangePollCorrectOptionCommandBot? changeCorrectOption;
    protected override void ExecuteOtherSteps(string commandStr)
    {
        if (changeCorrectOption is not null)
        {
            changeCorrectOption.Execute(commandStr);
            Poll = changeCorrectOption.Poll;
            IsStrResponse = false;
            IsFinished = true;

            return;
        }

        PollType? pollType = commandStr.ParseToPollType();
        if (pollType is null)
            return;
        else if (pollType == PollType.Regular)
        {
            Poll = PollHelper.ChangePollTypeIntoRegular(pollEditor);
            IsFinished = true;
        }
        else
        {
            Poll = pollEditor.ChangePollType(pollType.Value);

            changeCorrectOption = new ChangePollCorrectOptionCommandBot(Poll);
            changeCorrectOption.Execute(null);
            MessageStr = changeCorrectOption.MessageStr;
            ReplyMarkup = changeCorrectOption.ReplyMarkup;
            IsStrResponse = true;
        }
    }
}