using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PollEditorBot.Commands;

public class StartBotCommand : BaseBotCommand
{
    public override void Execute(string? commandStr)
    {
        string resultStr = string.Empty;

        resultStr += "Welcome! Here you can edit your polls";

        MessageStr = resultStr;
        IsStrResponse = true;
        IsFinished = true;
    }
}
