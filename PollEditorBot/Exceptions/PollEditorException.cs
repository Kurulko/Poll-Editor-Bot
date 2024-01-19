using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PollEditorBot.Exceptions;

public class PollEditorException : Exception
{
    public PollEditorException(string name) : base(name) { }

    public static PollEditorException InccorectLength(int minLength, int maxLength)
       => new($"Length must be between {minLength} and {maxLength} characters.");
    public static PollEditorException InccorectCountOfSeconds(int minCountOfSeconds, int maxCountOfSeconds)
       => new($"Count of seconds must be between {minCountOfSeconds} and {maxCountOfSeconds} characters.");
    public static PollEditorException TypeBooleanRequired()
       => new($"Value must be either <code>{true}</code> or <code>{false}</code>.");
    public static PollEditorException TypeNumberRequired()
       => new("Must be a number.");
    public static PollEditorException PollRequired()
       => new("To get started, send a poll.");
    public static PollEditorException CommandNotExist()
       => new("The command doesn't exist.");
    public static PollEditorException IncorrectOptionNumber(int maxOptionNumber)
       => new($"The option ID must be between 1 and {maxOptionNumber}.");
}
