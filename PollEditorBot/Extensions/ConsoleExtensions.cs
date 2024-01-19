using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace PollEditorBot.Extensions;

public static class ConsoleExtensions
{
    public static void WriteWithTextColor(string message, ConsoleColor textColor)
    {
        Console.ForegroundColor = textColor;
        Console.Write(message);
        Console.ResetColor();
    }

    public static void WriteWithBackgroundColor(string message, ConsoleColor backgroundColor)
    {
        Console.BackgroundColor = backgroundColor;
        Console.Write(message);
        Console.ResetColor();
    }
    public static void WriteWithTextAndBackgroundColor(string message, ConsoleColor textColor, ConsoleColor backgroundColor)
    {
        Console.ForegroundColor = textColor;
        Console.BackgroundColor = backgroundColor;

        Console.Write(message);
        Console.ResetColor();
    }
}