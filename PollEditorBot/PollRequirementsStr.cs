using PollEditorBot.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PollEditorBot;

public static class PollRequirementsStr
{
    public static readonly string PollOptionRequirementsHTMLStr = ToItalicsAndInBracketsHTML($"{TelegramSettings.MinPollOptionLength}-{TelegramSettings.MaxPollOptionLength} characters");

    public static readonly string PollOpenPeriodRequirementsHTMLStr = ToItalicsAndInBracketsHTML($"(more than {TelegramSettings.MinPollOpenPeriodInSeconds} and less than {TelegramSettings.MaxPollOpenPeriodInSeconds} seconds)");

    static string ToItalicsAndInBracketsHTML(string str)
        => $"<i>{str}</i>";
}
