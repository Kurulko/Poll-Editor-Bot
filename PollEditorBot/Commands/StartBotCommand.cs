using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PollEditorBot.Commands;

public class StartBotCommand : BaseBotCommand
{
    const string readmeLink = "https://github.com/Kurulko/Poll-Editor-Bot/blob/master/README.md";
    public override void Execute(string? commandStr)
    {
        string resultStr = string.Empty;

        resultStr += $"Welcome! Here you can edit your polls. To learn more, read <a href='{readmeLink}'>it</a>";

        MessageStr = resultStr;
        IsStrResponse = true;
        IsFinished = true;
    }
}
