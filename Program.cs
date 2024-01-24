// See https://aka.ms/new-console-template for more information

using Telegram.Bot;
using Telegram.Bot.Polling;
using TelegramBotTestProject;
using TelegramBotTestProject.Handlers;

DotNetEnv.Env.Load(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", ".env"));

void Main()
{
    var bot = TgBot.Bot;
    Console.WriteLine($"Запущен бот {bot.GetMeAsync().Result.FirstName}");

    var cts = new CancellationTokenSource();
    var cancellationToken = cts.Token;
    var receiverOptions = new ReceiverOptions
    {
        AllowedUpdates = { },
    };
    bot.StartReceiving(
        MessageHandler.HandleUpdateAsync,
        MessageHandler.HandleErrorAsync,
        receiverOptions,
        cancellationToken
    );
    Console.ReadLine();
}

Main();