using SpracheMeister.Repository;
using System.Collections.ObjectModel;
using SpracheMeister.Command;
using SpracheMeister.Model;

namespace SpracheMeister.ViewModel;

public class SettingsViewModel : ViewModelBase
{
    private float _temperature;
    private int _maxLength;
    private string? _selectedApiModel;
    private string? _template;
    private string? _apiKey;

    private readonly ISettingsRepository _settingsRepository;

    public DelegateCommand SaveCommand { get; }

    public ObservableCollection<string> ApiModels { get; } = new()
    {
        "gpt-4",
        "gpt-3.5-turbo"
    };


    public SettingsViewModel(ISettingsRepository settingsRepository)
    {
        _settingsRepository = settingsRepository;

        SaveCommand = new DelegateCommand(Save, CanSave);

        LoadSettings();
    }

    public string? ApiKey
    {
        get => _apiKey;
        set
        {
            _apiKey = value;
            RaisePropertyChanged();
            SaveCommand.RaiseCanExecuteChanged();
        }
    }

    public string? Template
    {
        get => _template;
        set
        {
            _template = value;
            RaisePropertyChanged();
            SaveCommand.RaiseCanExecuteChanged();
        }
    }

    public string? SelectedApiModel
    {
        get => _selectedApiModel;
        set
        {
            _selectedApiModel = value;
            RaisePropertyChanged();
            SaveCommand.RaiseCanExecuteChanged();
            SaveCommand.RaiseCanExecuteChanged();
        }
    }

    public float Temperature
    {
        get => _temperature;
        set
        {
            _temperature = value;
            RaisePropertyChanged();
        }
    }

    public int MaxLength
    {
        get => _maxLength;
        set
        {
            _maxLength = value;
            RaisePropertyChanged();
        }
    }

    private bool CanSave(object? arg) => !string.IsNullOrEmpty(ApiKey) 
                                         && !string.IsNullOrEmpty(Template)
                                         && !string.IsNullOrEmpty(SelectedApiModel);

    private void Save(object? obj)
    {
        var settings = new OpenAiSettings
        {
            ApiKey = ApiKey,
            Template = Template,
            ApiModel = SelectedApiModel,
            Temperature = Temperature,
            MaxLength = MaxLength,
        };
        
        _settingsRepository.Save(settings);
    }

    public void LoadSettings()
    {
        var settings = _settingsRepository.Load();
        ApiKey = settings.ApiKey;
        SelectedApiModel = settings.ApiModel;
        Temperature = settings.Temperature;
        MaxLength = settings.MaxLength;
        Template = settings.Template;
    }
}