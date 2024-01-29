using System.Net;
using DotNetEnv;
using OpenAI_API;
using OpenAI_API.Chat;
using OpenAI_API.Models;

namespace TelegramBotTestProject.Handlers;

public static class OpenAIHandler
{

    private static readonly string Token;
    private static OpenAIAPI api;
    
    static OpenAIHandler()
    {
        Token = Env.GetString("OPENAI_TOKEN");
        api = new OpenAIAPI(Token);
    }

    public static Conversation CreateConversation()
    {
        var conversation = api.Chat.CreateConversation();
        conversation.Model = Model.DefaultModel;
        conversation.RequestParameters.Temperature = 0;
        return conversation;
    }

    public static async Task<string> GetAnswer(Conversation chat, string message)
    {
        chat.AppendUserInput(message);
        return await chat.GetResponseFromChatbotAsync();
    }
    
    public static async Task<string> GetAnswer(Conversation chat, string message, string userName)
    {
        chat.AppendUserInputWithName(message, userName);
        return await chat.GetResponseFromChatbotAsync();
    }
}