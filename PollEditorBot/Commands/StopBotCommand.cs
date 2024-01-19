using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace PollEditorBot.Commands;

public class StopBotCommand : BaseBotCommand
{
    public override void Execute(string? commandStr)
    {
        string resultStr = string.Empty;

        resultStr += "Bye! Hope to see you soon";

        ReplyMarkup = new ReplyKeyboardRemove();
        MessageStr = resultStr;
        IsStrResponse = true;
        IsFinished = true;
    }
}
