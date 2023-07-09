using Azure.AI.OpenAI;
using SpracheMeister.Model;

namespace SpracheMeister.Repository;

public interface IOpenAiRepository
{
    public Task<string?> GetAnalysis(string? data, OpenAiSettings settings);
}

public class OpenAiRepository : IOpenAiRepository
{
    private const string DataPlaceholder = "<<DATA>>";

    public async Task<string?> GetAnalysis(string? data, OpenAiSettings settings)
    {
        ArgumentException.ThrowIfNullOrEmpty(nameof(data));
        ArgumentException.ThrowIfNullOrEmpty(nameof(settings));

        var prompt = settings.Template?.Replace(DataPlaceholder, data);

        var client = new OpenAIClient(settings.ApiKey, new OpenAIClientOptions());

        var chatCompletionsOptions = new ChatCompletionsOptions
        {
            Messages =
            {
                new ChatMessage(ChatRole.User, prompt),
            },
            Temperature = settings.Temperature,
            MaxTokens = settings.MaxLength
        };

        var response = await client.GetChatCompletionsAsync(
            deploymentOrModelName: settings.ApiModel,
            chatCompletionsOptions);

        var chatCompletions = response.Value;

        // we expect only one response
        return chatCompletions?.Choices?.FirstOrDefault()?.Message.Content;
    }
}