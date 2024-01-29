using OpenAI_API.Chat;
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
    private static bool ChatMode = false;
    private static Dictionary<string, Conversation> Chats = new Dictionary<string, Conversation>();

    public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update,
        CancellationToken cancellationToken)
    {
        Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(update));
        if (update.Type == UpdateType.Message)
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
            HandleMessage(message);
        }

        if (update.Type == UpdateType.CallbackQuery)
        {
            var codeOfButton = update.CallbackQuery!.Data ?? "";
            if (codeOfButton == "main")
            {
                Messages.SendMainMessage(update.CallbackQuery.Message!);
            }
            if (codeOfButton == "ChatGpt")
            {
                Messages.UpdateMessage(update.CallbackQuery.Message!, "Вы могли бы начать диалог с чатом ГПТ, но опен АИ просит за всё бабло");
                ChatMode = false;
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

    private static async void HandleMessage(Message message)
    {
        if (ChatMode)
        {
            if (Chats.ContainsKey(message.Chat.Username!))
            {
                await OpenAIHandler.GetAnswer(Chats[message.Chat.Username!], message.Text!);
            }
            else
            {
                Chats[message.Chat.Username!] = OpenAIHandler.CreateConversation();
                await OpenAIHandler.GetAnswer(Chats[message.Chat.Username!], message.Text!);
            }
        }
        else
        {
            Chats.Remove(message.Chat.Username!);
            Messages.SendMainMessage(message);
        }
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