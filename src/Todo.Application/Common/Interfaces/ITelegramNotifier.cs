namespace Todo.Application.Common.Interfaces;
public interface ITelegramNotifier
{
    Task SendMessage(long chatId, string text);
}