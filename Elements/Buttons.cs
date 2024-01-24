using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBotTestProject.Constants;

public static class Buttons
{
    public static InlineKeyboardButton Main =
        InlineKeyboardButton.WithCallbackData(text: "На главную", callbackData: "main");
    
    public static InlineKeyboardButton One =
        InlineKeyboardButton.WithCallbackData(text: "Кнопка 1", callbackData: "button1");
    public static InlineKeyboardButton Two =
        InlineKeyboardButton.WithCallbackData(text: "Кнопка 2", callbackData: "button2");

    public static InlineKeyboardButton
        GetJoke = InlineKeyboardButton.WithCallbackData("Прислать шутку (en)", "getJoke");
    public static InlineKeyboardButton
        AnotherJoke = InlineKeyboardButton.WithCallbackData("Ещё одна (en)", "getJoke");

    
    public static InlineKeyboardButton
        GetCatGif = InlineKeyboardButton.WithCallbackData("Прислать Котика", "GetCatGif");
    public static InlineKeyboardButton
        AnotherGif = InlineKeyboardButton.WithCallbackData("Давай другого!", "GetCatGif");

}