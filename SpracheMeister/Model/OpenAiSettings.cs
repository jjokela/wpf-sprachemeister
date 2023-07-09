namespace SpracheMeister.Model;

public record OpenAiSettings
{
    public string? ApiKey { get; set; }
    public string? Template { get; set; }
    public string? ApiModel { get; set; }
    public float Temperature { get; set; }
    public int MaxLength { get; set; }
}