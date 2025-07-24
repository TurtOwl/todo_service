using Microsoft.Extensions.Configuration;
using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;

var cfg = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: true)
    .AddEnvironmentVariables()
    .Build();

var bot = new TelegramBotClient(cfg["Telegram:Token"]!);
using var cts = new CancellationTokenSource();

var receiverOptions = new ReceiverOptions
{
    AllowedUpdates = Array.Empty<UpdateType>() // все типы апдейтов
};

bot.StartReceiving(
    updateHandler: async (botClient, upd, ct) =>
    {
        if (upd.Message is not { } msg) return;
        await botClient.SendTextMessageAsync(msg.Chat.Id,
            $"Echo: {msg.Text}", cancellationToken: ct);
    },
    pollingErrorHandler: (_, ex, _) => Console.WriteLine(ex),
    cancellationToken: cts.Token);

Console.WriteLine("Bot started. Press Ctrl+C to stop.");
Console.ReadKey();
cts.Cancel();