using PollEditorBot.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;
using PollEditorBot.Exceptions;

namespace PollEditorBot.Commands.TwoSteps;

public class ChangePollExplanationBotCommand : ChangeQuizBotCommand
{
    public ChangePollExplanationBotCommand(Poll poll) : base(poll) { }

    protected override void ExecuteQuizFirstStep()
            => MessageStr = $"Send the explanation";

    protected override void ExecuteSecondStep(string commandStr)
        => Poll = pollEditor.ChangePollExplanation(commandStr, MessageEntities);
}