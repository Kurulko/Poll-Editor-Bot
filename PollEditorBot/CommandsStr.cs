using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PollEditorBot;

public struct CommandsStr
{
    public const string Start = "/start";
    public const string Stop = "/stop";
    public const string Help = "/help";
    public const string ChangePollVisibility = "/change_visibility";
    public const string ChangePollQuestion = "/change_question";
    public const string ChangePollQuestionByTemplate = "/change_question_by_template";
    public const string ChangePollType = "/change_poll_type";
    public const string ChangePollCorrectOption = "/change_correct_option";
    public const string ChangePollExplanation = "/change_explanation";
    public const string ChangePollOpenPeriod = "/change_open_period";
    public const string DropPollOpenPeriod = "/drop_open_period";
    public const string GetTextPoll = "/get_text_poll";
    public const string CreatePoll = "/create_poll";
    public const string ChangePollOption = "/change_option";
    public const string ChangePollOptions = "/change_options";
    public const string ChangePollIsMultipleAnswers = "/change_is_multiple_answers";
    public const string DropPollExplanation = "/drop_explanation";
    public const string InsertPollOption = "/insert_option";
    public const string AddPollOptionToEnd = "/add_option_to_end";
    public const string DeletePollOption = "/delete_option";
}
