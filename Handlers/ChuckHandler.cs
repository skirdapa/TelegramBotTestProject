namespace TelegramBotTestProject.Handlers;

public static class ChuckHandler
{
    private static string Url = "https://geek-jokes.sameerkumar.website/api";
    private static readonly HttpClient Client = new ();
    
    public static async Task<string> GetJoke()
    {
        var joke = "";
        try
        {
            HttpResponseMessage response = await Client.GetAsync(Url);
            response.EnsureSuccessStatusCode();

            joke = await response.Content.ReadAsStringAsync();
        }
        catch (HttpRequestException e)
        {
            joke = "Шутка не получена(";
            Console.WriteLine(e);
        }
        return joke;
    }
}