using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PollEditorBot.Exceptions;

public class TelegramException : Exception
{
    public TelegramException(string name) : base(name) { }

    public static string TooManyRequests(int delayInSeconds)
       => $"Too many requests. Waiting for {delayInSeconds} seconds.";

    public const string QuizSentIncorrectly = "The quiz was sent incorrectly. The quiz must be submitted on your behalf and must be answered if it is not yours.";

    public const string MessageTypeNotSuitable = "The current message type is not suitable";
    public const string MessageEntityTypeNotSupported = "The message entity type is not supported for now";
}
