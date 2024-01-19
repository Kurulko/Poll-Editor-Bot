using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Extensions;
using Telegram.Bot.Polling;
using PollEditorBot.Extensions;
using PollEditorBot;
using PollEditorBot.Loggers;

ILogger logger = new ConsoleLogger();
PollEditorBotLauncher bot = new(logger);

CancellationTokenSource cts = new();

await bot.StartReceivingAsync(cts);

logger.LogInformationLine("Write something to finish the app");
Console.ReadLine();