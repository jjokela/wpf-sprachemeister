using SpracheMeister.Model;

namespace SpracheMeister.Repository;

public interface ISettingsRepository
{
    OpenAiSettings Load();
    void Save(OpenAiSettings openAiSettings);
}

public class SettingsRepository : ISettingsRepository
{
    public OpenAiSettings Load()
    {
        return new OpenAiSettings
        {
            ApiKey = Properties.Settings.Default.ApiKey,
            Template = Properties.Settings.Default.Template,
            ApiModel = Properties.Settings.Default.ApiModel,
            Temperature = Properties.Settings.Default.Temperature,
            MaxLength = Properties.Settings.Default.MaxLength,
        };
    }

    public void Save(OpenAiSettings openAiSettings)
    {
        Properties.Settings.Default.ApiKey = openAiSettings.ApiKey;
        Properties.Settings.Default.Template = openAiSettings.Template;
        Properties.Settings.Default.ApiModel = openAiSettings.ApiModel;
        Properties.Settings.Default.Temperature = openAiSettings.Temperature;
        Properties.Settings.Default.MaxLength = openAiSettings.MaxLength;

        Properties.Settings.Default.Save();
    }
}