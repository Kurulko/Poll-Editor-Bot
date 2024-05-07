using PollEditorBot.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PollEditorBot.Exceptions;

public class PollEditorException : Exception
{
    public PollEditorException(string name) : base(name) { }

    public static PollEditorException MinCountOfOptionsReached()
       => new($"The minimum count of options is {TelegramSettings.MinPollCountOfOptions}.");
    public static PollEditorException MaxCountOfOptionsReached()
       => new($"The maximum count of options is {TelegramSettings.MaxPollCountOfOptions}.");

    public static PollEditorException InccorectLength(int minLength, int maxLength)
       => new($"Length must be between {minLength} and {maxLength} characters.");
    public static PollEditorException InvalidLink()
       => new($"Link is invalid");
    public static PollEditorException InccorectCountOfSeconds(int minCountOfSeconds, int maxCountOfSeconds)
       => new($"Count of seconds must be between {minCountOfSeconds} and {maxCountOfSeconds} characters.");
    public static PollEditorException TypeBooleanRequired()
       => new($"Value must be either <code>{true}</code> or <code>{false}</code>.");
    public static PollEditorException TypeNumberRequired()
       => new("Must be a number.");
    public static PollEditorException PollRequired()
       => new("To get started, send a poll.");
    public static PollEditorException QuizRequiredToCommit()
       => new("The poll must be a quiz to commit.");
    public static PollEditorException CommandNotExist()
       => new("The command doesn't exist.");
    public static PollEditorException IncorrectOptionNumber(int maxOptionNumber)
       => new($"The option number must be between 1 and {maxOptionNumber}.");
}
