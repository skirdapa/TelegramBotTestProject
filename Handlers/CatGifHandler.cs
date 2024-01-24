using Telegram.Bot.Types;

namespace TelegramBotTestProject.Handlers;

public static class CatGifHandler
{
    private const string Url = "https://cataas.com/cat/gif";
    private static readonly HttpClient Client = new ();
    
    public static async Task<InputFile> GetCatGif()
    {
        var response = await Client.GetAsync(Url);
        return new InputFileStream(await response.Content.ReadAsStreamAsync(), "cat.gif");
    }
}