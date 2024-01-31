using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PollEditorBot.Loggers;

public abstract class Logger : ILogger
{
    public abstract void Print(string text);
    public abstract void Print(string sender, string text);

    public abstract void PrintWithTextColor(string colorText, ConsoleColor textColor, string sender, string text);
    public abstract void PrintWithTextColor(string colorText, ConsoleColor textColor, string text);

    public abstract void PrintWithBackgroundColor(string colorText, ConsoleColor backgroundColor, string sender, string text);
    public abstract void PrintWithBackgroundColor(string colorText, ConsoleColor backgroundColor, string text);

    public abstract void PrintWithTextAndBackgroundColor(string colorText, ConsoleColor textColor, ConsoleColor backgroundColor, string sender, string text);
    public abstract void PrintWithTextAndBackgroundColor(string colorText, ConsoleColor textColor, ConsoleColor backgroundColor, string text);


    public void LogDebug(string text)
        => Print(text);
    public void LogDebug(string sender, string text)
        => Print(sender, text);


    const string infoTemplateStr = "Info";
    public void LogInformation(string text)
        => PrintWithTextColor(LogTemplate(infoTemplateStr), ConsoleColor.Green, text);
    public void LogInformation(string sender, string text)
        => PrintWithTextColor(LogTemplate(infoTemplateStr), ConsoleColor.Green, sender, text);


    const string warningTemplateStr = "Warning";
    public void LogWarning(string text)
        => PrintWithTextColor(LogTemplate(warningTemplateStr), ConsoleColor.Yellow, text);
    public void LogWarning(string sender, string text)
        => PrintWithTextColor(LogTemplate(warningTemplateStr), ConsoleColor.Yellow, sender, text);


    const string errorTemplateStr = "Error";
    public void LogError(string text)
        => PrintWithTextAndBackgroundColor(LogTemplate(errorTemplateStr), ConsoleColor.Red, ConsoleColor.Black, text);
    public void LogError(string sender, string text)
        => PrintWithTextAndBackgroundColor(LogTemplate(errorTemplateStr), ConsoleColor.Red, ConsoleColor.Black, sender, text);


    const string criticalTemplateStr = "Critical";
    public void LogCritical(string text)
        => PrintWithBackgroundColor(LogTemplate(criticalTemplateStr), ConsoleColor.Red, text);
    public void LogCritical(string sender, string text)
        => PrintWithBackgroundColor(LogTemplate(criticalTemplateStr), ConsoleColor.Red, sender, text);

    private string LogTemplate(string str)
        => $"{str}: ";
}
