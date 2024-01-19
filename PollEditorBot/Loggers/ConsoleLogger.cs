using PollEditorBot.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PollEditorBot.Loggers;

public class ConsoleLogger : Logger
{

    public override void Print(string text)
        => Console.Write(text);

    public override void PrintWithBackgroundColor(string colorText, ConsoleColor backgroundColor, string text)
    {
        ConsoleExtensions.WriteWithBackgroundColor(colorText, backgroundColor);
        Print(text);
    }

    public override void PrintWithTextAndBackgroundColor(string colorText, ConsoleColor textColor, ConsoleColor backgroundColor, string text)
    {
        ConsoleExtensions.WriteWithTextAndBackgroundColor(colorText, textColor, backgroundColor);
        Print(text);
    }

    public override void PrintWithTextColor(string colorText, ConsoleColor textColor, string text)
    {
        ConsoleExtensions.WriteWithTextColor(colorText, textColor);
        Print(text);
    }
}
