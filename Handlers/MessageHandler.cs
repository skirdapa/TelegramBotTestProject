using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using TelegramBotTestProject.Constants;
using TelegramBotTestProject.Data;
using TelegramBotTestProject.Data.Table;
using Message = Telegram.Bot.Types.Message;
using Update = Telegram.Bot.Types.Update;

namespace TelegramBotTestProject.Handlers;

public static class MessageHandler
{
    public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update,
        CancellationToken cancellationToken)
    {
        Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(update));
        if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
        {
            var message = update.Message ?? new Message { Text = "" };
            if (message.Text!.ToLower() == "/start")
            {
                Messages.SendWelcome(message);
                SaveUser(message);
                return;
            }

            if (message.Text!.ToLower() == "/users")
            {
                Messages.SendUsers(message);
                return;
            }

            Messages.SendMainMessage(message);
        }

        if (update.Type == UpdateType.CallbackQuery)
        {
            var codeOfButton = update.CallbackQuery!.Data ?? "";
            if (codeOfButton == "main")
            {
                Messages.SendMainMessage(update.CallbackQuery.Message!);
            }
            if (codeOfButton == "button1")
            {
                Messages.UpdateMessage(update.CallbackQuery.Message!, "Вы нажали кнопку 1");
            }

            if (codeOfButton == "button2")
            {
                Messages.UpdateMessage(update.CallbackQuery.Message!, "Вы нажали кнопку 2");
            }

            if (codeOfButton == "getJoke")
            {
                Messages.SendJoke(update.CallbackQuery.Message!);
            }

            if (codeOfButton == "GetCatGif")
            {
               Messages.SendCatGif(update.CallbackQuery.Message!);
            }
        }
    }

    public static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception,
        CancellationToken cancellationToken)
    {
        Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(exception));
    }

    private static int SaveUser(Message message)
    {
        var user = new Users()
        {
            ID = message.Chat.Id,
            FirstName = message.Chat.FirstName,
            LastName = message.Chat.LastName,
            UserName = message.Chat.Username
        };
        return DB.InsertUsers(user);
    }
}