using PollEditorBot.Editors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types;

namespace PollEditorBot.Commands;

public abstract class PollBotCommand : BaseBotCommand
{
    protected readonly PollEditor pollEditor;
    public PollBotCommand(Poll poll)
    {
        pollEditor = new(poll);
        Poll = poll;
    }
}
