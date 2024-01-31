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

    public static readonly string QuizSentIncorrectly = ErrorTypeStr("QUIZ_SENT_INCORRECTLY", "The quiz was sent incorrectly. The quiz must be submitted on your behalf and must be answered if it is not yours");

    public static readonly string MessageTypeNotSuitable = ErrorTypeStr("MESSAGE_TYPE_NOT_SUITABLE", "The current message type is not suitable");

    public static readonly string MessageEntityTypeNotSupported = ErrorTypeStr("MESSAGE_ENTITY_TYPE_NOT_SUPPORTED", "The message entity type is not supported for now");

    public static readonly string OnlyPrivateChatsSupported = ErrorTypeStr("ONLY_PRIVATE_CHATS_SUPPORTED", "This bot can be used only in private chats");

    const string readmeErrorsLink = "https://github.com/Kurulko/Poll-Editor-Bot/blob/master/README.md#errors";
    static string ErrorTypeStr(string type, string error)
        => $"❌ <b>{type}</b>: {error}. To learn more, read <a href='{readmeErrorsLink}'>it</a>";
}
