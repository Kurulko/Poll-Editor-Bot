using PollEditorBot.Editors;
using PollEditorBot.Enums;
using PollEditorBot.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace PollEditorBot.Commands;

public abstract class BaseBotCommand
{
    public bool? IsFinished { get; protected set; }
    public bool IsStrResponse { get; protected set; }
    public string? MessageStr { get; protected set; }
    public Poll Poll { get; protected set; } = new();
    public IEnumerable<MessageEntity>? MessageEntities { get; set; }
    public IReplyMarkup? ReplyMarkup { get; protected set; } = new ReplyKeyboardRemove();

    public abstract void Execute(string? commandStr);

    protected virtual void CopyValues(BaseBotCommand botCommand)
    {
        IsStrResponse = botCommand.IsStrResponse;
        IsFinished = botCommand.IsFinished;
        Poll = botCommand.Poll;
        MessageStr = botCommand.MessageStr;
        ReplyMarkup = botCommand.ReplyMarkup;
    }
}
