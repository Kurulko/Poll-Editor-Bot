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
    public override void Print(string sender, string text)
        => Print($"{sender} - {text}");

    private void PrintWithBackgroundColor(string colorText, ConsoleColor backgroundColor)
        => ConsoleExtensions.WriteWithBackgroundColor(colorText, backgroundColor);
    public override void PrintWithBackgroundColor(string colorText, ConsoleColor backgroundColor, string text)
    {
        PrintWithBackgroundColor(colorText, backgroundColor);
        Print(text);
    }
    public override void PrintWithBackgroundColor(string colorText, ConsoleColor backgroundColor, string sender, string text)
    {
        PrintWithBackgroundColor(colorText, backgroundColor);
        Print(sender, text);
    }

    private void PrintWithTextAndBackgroundColor(string colorText, ConsoleColor textColor, ConsoleColor backgroundColor)
        => ConsoleExtensions.WriteWithTextAndBackgroundColor(colorText, textColor, backgroundColor);
    public override void PrintWithTextAndBackgroundColor(string colorText, ConsoleColor textColor, ConsoleColor backgroundColor, string text)
    {
        PrintWithTextAndBackgroundColor(colorText, textColor, backgroundColor);
        Print(text);
    }
    public override void PrintWithTextAndBackgroundColor(string colorText, ConsoleColor textColor, ConsoleColor backgroundColor, string sender, string text)
    {
        PrintWithTextAndBackgroundColor(colorText, textColor, backgroundColor);
        Print(sender, text);
    }

    private void PrintWithTextColor(string colorText, ConsoleColor textColor)
        => ConsoleExtensions.WriteWithTextColor(colorText, textColor);
    public override void PrintWithTextColor(string colorText, ConsoleColor textColor, string text)
    {
        PrintWithTextColor(colorText, textColor);
        Print(text);
    }
    public override void PrintWithTextColor(string colorText, ConsoleColor textColor, string sender, string text)
    {
        PrintWithTextColor(colorText, textColor);
        Print(sender, text);
    }
}
