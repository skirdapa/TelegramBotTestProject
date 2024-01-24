using Telegram.Bot;
using DotNetEnv;

namespace TelegramBotTestProject;

public static class TgBot
{
    private static readonly string Token;

    public static readonly ITelegramBotClient Bot;

    static TgBot()
    {
        Token = Env.GetString("TG_BOT_TOKEN");
        Bot = new TelegramBotClient(Token);
    }
}