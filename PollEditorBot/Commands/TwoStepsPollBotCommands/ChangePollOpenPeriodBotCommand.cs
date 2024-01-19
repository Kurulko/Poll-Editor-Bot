using PollEditorBot.Enums;
using PollEditorBot.Exceptions;
using PollEditorBot.Extensions;
using PollEditorBot.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace PollEditorBot.Commands.TwoSteps;

public class ChangePollOpenPeriodBotCommand : TwoStepsPollBotCommand
{
    public ChangePollOpenPeriodBotCommand(Poll poll) : base(poll) { }

    protected override void ExecuteFirstStep()
        => MessageStr = $"Send seconds counter when poll closes {PollRequirementsStr.PollOpenPeriodRequirementsHTMLStr}";

    protected override void ExecuteSecondStep(string commandStr)
    {
        if (int.TryParse(commandStr, out int countOfSeconds))
        {    
            if(countOfSeconds >= TelegramSettings.MinPollOpenPeriodInSeconds && countOfSeconds <= TelegramSettings.MaxPollOpenPeriodInSeconds)
                Poll = pollEditor.ChangePollOpenPeriod(countOfSeconds);
            else
                throw PollEditorException.InccorectCountOfSeconds(TelegramSettings.MinPollOptionLength, TelegramSettings.MaxPollOptionLength);
        } 
        else
            throw PollEditorException.TypeNumberRequired();
    }
}