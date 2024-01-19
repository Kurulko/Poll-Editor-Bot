using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot;

namespace PollEditorBot.Settings;

public class TelegramSettings
{
    public const int MinPollOptionLength = 1, MaxPollOptionLength = 100;
    public const int MinPollOpenPeriodInSeconds = 5, MaxPollOpenPeriodInSeconds = 600;

    public static int MaxLengthOfMessage => 4000;
    static string Token => "6931988250:AAFDCXQo_LxMKV3k6TZxDB1t87GAKIhCstI";
    static ITelegramBotClient bot = null!;

    public static ITelegramBotClient CurrentBot()
    {
        if (bot == null)
            bot = new TelegramBotClient(Token);
        return bot;
    }

    public static async Task<string?> GetCurrentBotName()
    {
        var bot = CurrentBot();
        User user = await bot.GetMeAsync();
        return user.Username;
    }
}
