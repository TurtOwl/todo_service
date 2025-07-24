using Telegram.Bot;
using Telegram.Bot.Types;
using Todo.Application.Common.Interfaces;

namespace Todo.Infrastructure.Services;

public class TelegramBotNotifier : ITelegramNotifier
{
    private readonly ITelegramBotClient _bot;
    public TelegramBotNotifier(IConfiguration cfg)
        => _bot = new TelegramBotClient(cfg["Telegram:Token"]);

    public async Task SendMessage(long chatId, string text)
        => await _bot.SendTextMessageAsync(new ChatId(chatId), text);
}