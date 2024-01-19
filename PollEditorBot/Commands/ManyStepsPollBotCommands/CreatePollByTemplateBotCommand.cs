using PollEditorBot.Commands.ManySteps;
using PollEditorBot.Commands.TwoSteps;
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

namespace PollEditorBot.Commands.ManyStepsPollBotCommands;

public class CreatePollBotCommand : ManyStepsPollBotCommand
{
    public CreatePollBotCommand() : base(new() { Type = PollType.Regular.ToLowerCaseString() }) { }

    BaseBotCommand? botCommand;
    protected override void ExecuteFirstStep()
    {
        botCommand = new ChangePollVisibilityBotCommand(Poll);
        botCommand.Execute(null);
        MessageStr = botCommand.MessageStr;
        ReplyMarkup = botCommand.ReplyMarkup;
    }

    protected override void ExecuteOtherSteps(string commandStr)
    {
        IsStrResponse = true;

        if (botCommand is ChangePollVisibilityBotCommand)
            ExecuteBotCommand(() => new ChangeOptionsBotCommand(Poll), commandStr);
        else if (botCommand is ChangeOptionsBotCommand)
            ExecuteBotCommand(() => new ChangePollTypeBotCommand(Poll), commandStr);
        else if (botCommand is ChangePollTypeBotCommand)
            ExecuteBotCommand(() => new ChangePollQuestionBotCommand(Poll), commandStr);
        else
        {
            if (PollHelper.IsQuiz(Poll))
            {
                if (botCommand is ChangePollQuestionBotCommand)
                    ExecuteBotCommand(() => new ChangePollExplanationBotCommand(Poll), commandStr, true);
                else if (botCommand is ChangePollExplanationBotCommand)
                    ExecuteBotCommand(() => new ChangePollOpenPeriodBotCommand(Poll), commandStr, true);
                else if (botCommand is ChangePollOpenPeriodBotCommand)
                    ExecuteBotCommand(null, commandStr, true);
            }
            else if (botCommand is not null)
            {
                if (botCommand is ChangePollQuestionBotCommand)
                    ExecuteBotCommand(() => new ChangePollOpenPeriodBotCommand(Poll), commandStr, true);
                else if (botCommand is ChangePollOpenPeriodBotCommand)
                    ExecuteBotCommand(null, commandStr, true);
            }
        }
    }

    const string skip = "Skip";
    readonly IEnumerable<KeyboardButton> SkipKeyboardButton = new List<KeyboardButton>() { new(skip) };
    void ExecuteBotCommand(Func<BaseBotCommand>? createBotCommand, string commandStr, bool isSkippedCommand = false) 
    {
        if (botCommand is not null)
        {
            bool isSkip = isSkippedCommand && commandStr == skip;

            if (!isSkip && !(botCommand.IsFinished ?? false))
            {
                botCommand.Execute(commandStr);
                Poll = botCommand.Poll;
                MessageStr = botCommand.MessageStr;
            }

            if (isSkip || (botCommand.IsFinished ?? false))
            {
                if(createBotCommand is null)
                {
                    Poll = botCommand.Poll;
                    IsStrResponse = false;
                    IsFinished = true;
                    ReplyMarkup = new ReplyKeyboardRemove();

                    return;
                }
                
                botCommand = createBotCommand();
                botCommand.Execute(null);
                MessageStr = botCommand.MessageStr;
            }

            AddReplyMarkup(isSkippedCommand);
        }
    }

    void AddReplyMarkup(bool isSkippedCommand)
    {
        var botCommandReplyMarkup = botCommand!.ReplyMarkup;
        if (isSkippedCommand)
        {
            if (botCommandReplyMarkup is ReplyKeyboardMarkup botCommandReplyKeyboardMarkup)
            {
                var keyboard = botCommandReplyKeyboardMarkup.Keyboard.ToList();
                keyboard.Add(SkipKeyboardButton);
                ReplyMarkup = new ReplyKeyboardMarkup(keyboard) { ResizeKeyboard = true };
            }
            else
            {
                ReplyMarkup = new ReplyKeyboardMarkup(SkipKeyboardButton) { ResizeKeyboard = true };
            }
        }
        else
        {
            ReplyMarkup = botCommandReplyMarkup;
        }
    }
}