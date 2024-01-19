using PollEditorBot.Editors;
using PollEditorBot.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace PollEditorBot;

public static class PollHelper
{
    public static IEnumerable<KeyboardButton> KeyboardNumberButtons(int countOfOptions)
        => Enumerable.Range(1, countOfOptions).Select(n => new KeyboardButton(n.ToString()));

    public static bool IsQuiz(Poll poll)
        => poll.Type.ToLower() == PollType.Quiz.ToLowerCaseString();// && poll.CorrectOptionId is not null;
    
    public static int CountOfOptions(Poll poll)
        => poll.Options.Length;

    public static Poll ChangePollTypeIntoRegular(PollEditor pollEditor)
    {
        pollEditor.ChangePollType(PollType.Regular);
        pollEditor.ChangePollCorrectOptionId(null);
        return pollEditor.ChangePollExplanation(null, null);
    }

    public static bool IfQuizSentCorrectly(Poll poll)
    {
        if(IsQuiz(poll))
            return poll.CorrectOptionId is not null;

        return true;
    }
}
