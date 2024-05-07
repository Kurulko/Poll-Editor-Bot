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

namespace PollEditorBot.Commands.ManyStepsPollBotCommands;

public class AddLinkToPollCommand : ManyStepsPollBotCommand
{
    public AddLinkToPollCommand(Poll poll) : base(poll) { }

    string linkStr = null!;
    protected override void ExecuteFirstStep()
        => MessageStr = $"Send the link.";

    protected override void ExecuteOtherSteps(string commandStr)
    {
        if (linkStr is null)
        {
            if (Uri.IsWellFormedUriString(commandStr, UriKind.Absolute))
            {
                linkStr = commandStr;
                MessageStr = $"Send the text.";
            }
            else
                throw PollEditorException.InvalidLink();
        }
        else
        {
            ReplyMarkup = new InlineKeyboardMarkup(new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithUrl(commandStr, linkStr)
                }
            });
            IsStrResponse = false;
            IsFinished = true;
        }
    }
}
