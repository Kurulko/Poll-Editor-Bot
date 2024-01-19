using PollEditorBot.Exceptions;
using PollEditorBot.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace PollEditorBot.Commands.TwoSteps;

public class AddPollOptionToEndBotCommand : TwoStepsPollBotCommand
{
    public AddPollOptionToEndBotCommand(Poll poll) : base(poll) { }

    protected override void ExecuteFirstStep()
            => MessageStr = $"Send a poll option which you want to add to the end of the list {PollRequirementsStr.PollOptionRequirementsHTMLStr}.";

    protected override void ExecuteSecondStep(string commandStr)
    {
        int length = commandStr.Length;
        if (length >= TelegramSettings.MinPollOptionLength && length <= TelegramSettings.MaxPollOptionLength)
            Poll = pollEditor.AddPollOption(commandStr!);
        else
            throw PollEditorException.InccorectLength(TelegramSettings.MinPollOptionLength, TelegramSettings.MaxPollOptionLength);
    }
}