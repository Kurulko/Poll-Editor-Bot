using PollEditorBot.Commands;
using PollEditorBot.Commands.ManySteps;
using PollEditorBot.Commands.ManyStepsPollBotCommands;
using PollEditorBot.Commands.OneStep;
using PollEditorBot.Commands.TwoSteps;
using PollEditorBot.Exceptions;
using Telegram.Bot.Types;

namespace PollEditorBot;

public class MessageReceiver
{
    readonly Poll? poll;
    public MessageReceiver() { }
    public MessageReceiver(Poll poll)
        => this.poll = poll;

    public BaseBotCommand? BotCommand { get; private set; }
    bool IsFinished => BotCommand?.IsFinished ?? true;

    public void Execute(string commandStr)
    {
        bool isBotCommand = commandStr.StartsWith("/");
        if (IsFinished || isBotCommand)
        {
            if (IsPollBotCommand(commandStr))
            {
                BotCommand = commandStr switch
                {
                    CommandsStr.ChangePollVisibility => new ChangePollVisibilityBotCommand(poll!),
                    CommandsStr.ChangePollQuestion => new ChangePollQuestionBotCommand(poll!),
                    CommandsStr.ChangePollQuestionByTemplate => new ChangePollQuestionByTemplateBotCommand(poll!),
                    CommandsStr.ChangePollType => new ChangePollTypeBotCommand(poll!),
                    CommandsStr.ChangePollIsMultipleAnswers => new ChangeIsMultipleAnswersBotCommand(poll!),
                    CommandsStr.ChangePollCorrectOption => new ChangePollCorrectOptionCommandBot(poll!),
                    CommandsStr.ChangePollExplanation => new ChangePollExplanationBotCommand(poll!),
                    CommandsStr.ChangePollOpenPeriod => new ChangePollOpenPeriodBotCommand(poll!),
                    CommandsStr.DropPollOpenPeriod => new DropPollOpenPeriodBotCommand(poll!),
                    CommandsStr.GetTextPoll => new GetTextPollBotCommand(poll!),
                    CommandsStr.ChangePollOption => new ChangeOptionBotCommand(poll!),
                    CommandsStr.ChangePollOptions => new ChangeOptionsBotCommand(poll!),
                    CommandsStr.DropPollExplanation => new DropPollExplanationBotCommand(poll!),
                    CommandsStr.InsertPollOption => new InsertPollOptionBotCommand(poll!),
                    CommandsStr.AddPollOptionToEnd => new AddPollOptionToEndBotCommand(poll!),
                    CommandsStr.DeletePollOption => new DeletePollOptionBotCommand(poll!),
                    _ => throw PollEditorException.PollRequired()
                };
            }
            else
            {
                BotCommand = commandStr switch
                {
                    CommandsStr.Start => new StartBotCommand(),
                    CommandsStr.Stop => new StopBotCommand(),
                    CommandsStr.Help => new HelpBotCommand(),
                    CommandsStr.CreatePoll => new CreatePollBotCommand(),
                    _ => throw PollEditorException.CommandNotExist()
                };
            }
        }
      
        BotCommand?.Execute(IsFinished ? null : commandStr);
    }

    bool IsPollBotCommand(string commandStr)
    {
        bool isPollBotCommand;

        switch (commandStr)
        {
            case CommandsStr.ChangePollVisibility :
            case CommandsStr.ChangePollQuestion:
            case CommandsStr.ChangePollQuestionByTemplate:
            case CommandsStr.ChangePollType:
            case CommandsStr.ChangePollIsMultipleAnswers:
            case CommandsStr.ChangePollCorrectOption:
            case CommandsStr.ChangePollExplanation:
            case CommandsStr.ChangePollOpenPeriod:
            case CommandsStr.DropPollOpenPeriod:
            case CommandsStr.GetTextPoll:
            case CommandsStr.ChangePollOption:
            case CommandsStr.ChangePollOptions:
            case CommandsStr.DropPollExplanation:
            case CommandsStr.AddPollOptionToEnd:
            case CommandsStr.InsertPollOption:
            case CommandsStr.DeletePollOption:
                isPollBotCommand = true;
                break;
            case CommandsStr.Start:
            case CommandsStr.Stop:
            case CommandsStr.Help:
            case CommandsStr.CreatePoll:
                isPollBotCommand = false;
                break;
            default:
                throw PollEditorException.CommandNotExist();
        };

        if (isPollBotCommand && poll is null)
            throw PollEditorException.PollRequired();

        return isPollBotCommand;
    }
}
