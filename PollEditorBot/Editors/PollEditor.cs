using PollEditorBot.Enums;
using PollEditorBot.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace PollEditorBot.Editors;

public class PollEditor
{
    readonly Poll poll;
    public PollEditor(Poll poll)
        => this.poll = poll;

    public Poll ChangePollVisibility(VisibilityType visibilityType)
    {
        poll.IsAnonymous = visibilityType == VisibilityType.Anonymous;
        return poll;
    }

    public Poll ChangePollIsMultipleAnswers(bool isMultipleAnswers)
    {
        poll.AllowsMultipleAnswers = isMultipleAnswers;
        return poll;
    }

    public Poll ChangePollType(PollType? pollType = null)
    {
        if(pollType is null)
            poll.Type = (poll.Type == PollType.Quiz.ToLowerCaseString() ? PollType.Quiz : PollType.Regular).ToLowerCaseString();
        else
            poll.Type = pollType.ToLowerCaseString();

        return poll;
    }

    public Poll ChangePollQuestion(string questionStr)
    {
        poll.Question = questionStr;
        return poll;
    }

    public Poll ChangePollQuestionByTemplate(string template)
    {
        poll.Question = string.Format(template, poll.Question);
        return poll;
    }
    
    public Poll ChangePollCloseDate(DateTime? closeDate)
    {
        poll.CloseDate = closeDate;
        return poll;
    }

    public Poll ChangePollOpenPeriod(int? openPeriodInSeconds)
    {
        poll.OpenPeriod = openPeriodInSeconds;
        return poll;
    }

    public Poll ChangePollCorrectOptionId(int? correctOptionId)
    {
        if (PollHelper.IsQuiz(poll))
            poll.CorrectOptionId = correctOptionId;
        return poll;
    }

    public Poll ChangePollExplanation(string? explanationStr, IEnumerable<MessageEntity>? explanationEntities)
    {
        if (PollHelper.IsQuiz(poll))
        {
            poll.Explanation = explanationStr;
            poll.ExplanationEntities = explanationEntities?.ToArray();
        }

        return poll;
    }

    public Poll ChangePollOptions(string[] optionsStr)
    {
        poll.Options = optionsStr.Select(optStr => new PollOption() { Text = optStr}).ToArray();
        return poll;
    }

    public Poll ChangePollOption(string optionStr, int index)
    {
        if(index < poll.Options.Length && index >= 0)
            poll.Options[index] = new PollOption() { Text = optionStr };
        return poll;
    }

    public Poll AddPollOption(string optionStr)
    {
        poll.Options = poll.Options.Append(new PollOption() { Text = optionStr }).ToArray();
        return poll;
    }

    public Poll InsertPollOption(string optionStr, int index)
    {
        var optionsList = new List<PollOption>(poll.Options);
        optionsList.Insert(index, new PollOption() { Text = optionStr });
        poll.Options = optionsList.ToArray();
        return poll;
    }

    public Poll DeletePollOption(int index)
    {
        if (index < poll.Options.Length && index >= 0)
        {
            var optionsList = poll.Options.ToList();
            optionsList.RemoveAt(index);
            poll.Options = optionsList.ToArray();
        }
        return poll;
    }
}
