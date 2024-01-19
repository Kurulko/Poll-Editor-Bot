using PollEditorBot.Enums;
using PollEditorBot.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace PollEditorBot.Commands.TwoSteps;

public class ChangePollVisibilityBotCommand : TwoStepsPollBotCommand
{
    public ChangePollVisibilityBotCommand(Poll poll) : base(poll) { }

    protected override void ExecuteFirstStep()
    {
        MessageStr = "Now send either <code>Anonymous</code>(anonymous poll) or <code>Public</code>(public poll)";
        IEnumerable<KeyboardButton> keyboardButtons = new List<KeyboardButton>()
        {
            new("Anonymous"), new("Public")
        };
        ReplyMarkup = new ReplyKeyboardMarkup(keyboardButtons) { ResizeKeyboard = true };
    }

    protected override void ExecuteSecondStep(string commandStr)
    {
        VisibilityType? visibilityType = commandStr.ParseToVisibilityType();
        if (visibilityType is null)
            return;

        Poll = pollEditor.ChangePollVisibility(visibilityType.Value);
    }
}
