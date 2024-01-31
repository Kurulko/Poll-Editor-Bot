using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PollEditorBot.Loggers;

public interface ILogger
{
    void LogDebug(string text);
    void LogDebug(string sender, string text);
    void LogDebugLine(string sender, string text)
        => LogDebug(sender, ToLine(text));
    void LogDebugLine(string text)
        => LogDebug(ToLine(text));

    void LogInformation(string sender, string text);
    void LogInformationLine(string sender, string text)
        => LogInformation(sender, ToLine(text));
    void LogInformation(string text);
    void LogInformationLine(string text)
        => LogInformation(ToLine(text));

    void LogWarning(string sender, string text);
    void LogWarningLine(string sender, string text)
        => LogWarning(sender, ToLine(text));
    void LogWarning(string text);
    void LogWarningLine(string text)
        => LogWarning(ToLine(text));

    void LogError(string sender, string text);
    void LogErrorLine(string sender, string text)
        => LogError(sender, ToLine(text));
    void LogError(string text);
    void LogErrorLine(string text)
        => LogError(ToLine(text));

    void LogCritical(string sender, string text);
    void LogCriticalLine(string sender, string text)
        => LogCritical(sender, ToLine(text));
    void LogCritical(string text);
    void LogCriticalLine(string text)
        => LogCritical(ToLine(text));

    private string ToLine(string text)
        => text + '\n';
}
