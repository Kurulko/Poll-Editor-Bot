using PollEditorBot.Commands.TwoSteps;
using PollEditorBot.Exceptions;
using PollEditorBot.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace PollEditorBot.Commands.ManySteps;

public class ChangeOptionsBotCommand : ManyStepsPollBotCommand
{
    public ChangeOptionsBotCommand(Poll poll) : base(poll) { }

    protected override void ExecuteFirstStep()
        => MessageStr = $"Send options, each new option on a new line {PollRequirementsStr.PollOptionRequirementsHTMLStr}.";

    bool IsQuiz => PollHelper.IsQuiz(Poll);

    int? countOfOptions;
    PollBotCommand? changeCorrectOption;
    protected override void ExecuteOtherSteps(string commandStr)
    {
        string[] options = { };
        if (countOfOptions is null)
        {
            options = commandStr.Split('\n');
            int length = options.Length;

            if (length > TelegramSettings.MaxPollCountOfOptions)
                throw PollEditorException.MaxCountOfOptionsReached();
            else if(length < TelegramSettings.MinPollCountOfOptions)
                throw PollEditorException.MinCountOfOptionsReached();
            else
                Poll = pollEditor.ChangePollOptions(options);
        }

        if (!IsQuiz)
        {
            IsFinished = true;
        }
        else
        {
            if (changeCorrectOption is null)
            {
                if (options.Any(optionStr =>
                {
                    int optionLenght = optionStr.Length;
                    return optionLenght < TelegramSettings.MinPollOptionLength || optionLenght > TelegramSettings.MaxPollOptionLength;
                }))
                    throw PollEditorException.InccorectLength(TelegramSettings.MinPollOptionLength, TelegramSettings.MaxPollOptionLength);

                countOfOptions = options.Length;
                changeCorrectOption = new ChangePollCorrectOptionCommandBot(Poll);
                changeCorrectOption.Execute(null);
            }
            else
            {
                changeCorrectOption.Execute(commandStr);
            }

            CopyValues(changeCorrectOption);
        }
    }
}