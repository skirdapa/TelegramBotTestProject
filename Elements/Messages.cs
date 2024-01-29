using OpenAI_API.Chat;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBotTestProject.Data;
using TelegramBotTestProject.Handlers;

namespace TelegramBotTestProject.Constants;

public static class Messages
{
    public static async void SendWelcome(Message message)
    {
        await TgBot.Bot.SendTextMessageAsync(message.Chat,
            $"Добро пожаловать, {message.Chat.FirstName ?? message.Chat.Username}!");
    }

    public static async void GetNewMessageFromGPT(Message message, Conversation chat, string userName)
    {
        var keyboard = MakeButtonsMarkup(Buttons.Main);
        await TgBot.Bot.SendTextMessageAsync(message.Chat,
            await OpenAIHandler.GetAnswer(chat, message.Text!, userName), replyMarkup: keyboard);
    }

    public static async void SendUsers(Message message)
    {
        await TgBot.Bot.SendTextMessageAsync(message.Chat,
            PrintUsers());
    }

    public static async void SendMainMessage(Message message)
    {
        var keyboard = MakeButtonsMarkup(Buttons.StartChat, Buttons.Two, Buttons.GetJoke, Buttons.GetCatGif);
        await TgBot.Bot.SendTextMessageAsync(
            message.Chat,
            "Сделай свой выбор",
            replyMarkup: keyboard
        );
    }

    public static async void SendJoke(Message message)
    {
        var keyboard = MakeButtonsMarkup(Buttons.Main, Buttons.AnotherJoke);
        await TgBot.Bot.EditMessageTextAsync(message.Chat.Id,
            message.MessageId, message.Text!);
        await TgBot.Bot.SendTextMessageAsync(message.Chat, await ChuckHandler.GetJoke(), replyMarkup: keyboard);
    }

    public static async void SendCatGif(Message message)
    {
        var keyboard = MakeButtonsMarkup(Buttons.Main, Buttons.AnotherGif);
        await TgBot.Bot.SendAnimationAsync(message!.Chat.Id,
            await CatGifHandler.GetCatGif(), replyMarkup: keyboard);
        await TgBot.Bot.EditMessageReplyMarkupAsync(message.Chat.Id,
            message.MessageId, replyMarkup: MakeButtonsMarkup(Buttons.Main));
    }

    public static async void UpdateMessage(Message message, string text = "Кнопка нажата")
    {
        await TgBot.Bot.EditMessageTextAsync(message.Chat.Id,
            message.MessageId, text, replyMarkup: MakeButtonsMarkup(Buttons.Main));
    }

    public static async void DeleteMessage(Message message)
    {
        await TgBot.Bot.DeleteMessageAsync(message.Chat, message.MessageId);
    }

    private static string PrintUsers()
    {
        var users = "";
        foreach (var user in DB.GetUsers().Take(10))
        {
            users += $"{user}\n";
        }

        return users;
    }

    public static InlineKeyboardMarkup MakeButtonsMarkup(params InlineKeyboardButton[] values)
    {
        var keyboard = new List<List<InlineKeyboardButton>>();
        if (values.Length % 2 != 0)
        {
            var firstList = new List<InlineKeyboardButton> { values.Last() };
            keyboard.Add(firstList);
        }

        for (var i = 0; i < values.Length - 1; i += 2)
        {
            var innerList = new List<InlineKeyboardButton>();
            for (var j = 0; j < 1; j++)
            {
                innerList.Add(values[i]);
                innerList.Add(values[i + 1]);
                keyboard.Add(innerList);
            }
        }

        return new InlineKeyboardMarkup(
            keyboard
        );
    }
}