using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PollEditorBot.Loggers;

public abstract class Logger : ILogger
{
    public abstract void Print(string text);
    public abstract void PrintWithTextColor(string colorText, ConsoleColor textColor, string text);
    public abstract void PrintWithBackgroundColor(string colorText, ConsoleColor backgroundColor, string text);
    public abstract void PrintWithTextAndBackgroundColor(string colorText, ConsoleColor textColor, ConsoleColor backgroundColor, string text);

    public void LogDebug(string text)
        => Print(text);

    public void LogInformation(string text)
        => PrintWithTextColor("Info: ", ConsoleColor.Green, text);

    public void LogWarning(string text)
        => PrintWithTextColor("Warning: ", ConsoleColor.Yellow, text);

    public void LogError(string text)
        => PrintWithTextAndBackgroundColor("Error: ", ConsoleColor.Red, ConsoleColor.Black, text);

    public void LogCritical(string text)
        => PrintWithBackgroundColor("Critical: ", ConsoleColor.Red, text);
}
