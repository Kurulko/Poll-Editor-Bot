using PollEditorBot.Editors;
using PollEditorBot.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace PollEditorBot.Extensions;

public static class StringExtensions
{
    public static IEnumerable<string> DevideStrIfMoreMaxLength(this string str, int maxLength)
    {
        int strLength = str.Length;
        if (strLength > maxLength)
        {
            int count = Convert.ToInt32(Math.Ceiling((double)strLength / maxLength));

            IList<string> res = new List<string>();
            for (int i = 0, sumOfLength = 0; i < count; i++, sumOfLength += maxLength)
            {
                if (i != count - 1)
                    res.Add(string.Concat(str.Skip(i * maxLength).Take(maxLength)));
                else
                    res.Add(string.Concat(str.Skip(i * maxLength).Take(strLength - sumOfLength)));
            }

            return res;
        }

        return new List<string> { str };
    }

    public static string ToLowerCaseString<T>(this T obj)
        => obj!.ToString()!.ToLower();

    public static VisibilityType? ParseToVisibilityType(this string? visibilityTypeStr)
    {
        string? visibilityTypeStrLower = visibilityTypeStr?.ToLower();
        if (visibilityTypeStrLower == VisibilityType.Anonymous.ToLowerCaseString())
            return VisibilityType.Anonymous;
        else if (visibilityTypeStrLower == VisibilityType.Public.ToLowerCaseString())
            return VisibilityType.Public;
        else
            return null;
    }

    public static PollType? ParseToPollType(this string? pollTypeStr)
    {
        string? pollTypeStrLower = pollTypeStr?.ToLower();
        if (pollTypeStrLower == PollType.Quiz.ToLowerCaseString())
            return PollType.Quiz;
        else if (pollTypeStrLower == PollType.Regular.ToLowerCaseString())
            return PollType.Regular;
        else
            return null;
    }
}
