using SpracheMeister.Command;
using SpracheMeister.Repository;

namespace SpracheMeister.ViewModel;

public class TranslatorViewModel : ViewModelBase
{
    private readonly IOpenAiRepository _openAiRepository;
    private readonly ISettingsRepository _settingsRepository;

    private string? _inputText;
    private string? _outputText;
    private bool _isLoading;

    public DelegateCommand SubmitCommand { get; }

    public TranslatorViewModel(IOpenAiRepository openAiRepository, ISettingsRepository settingsRepository)
    {
        _openAiRepository = openAiRepository;
        _settingsRepository = settingsRepository;

        SubmitCommand = new DelegateCommand(Submit, CanSubmit);
    }

    public string? InputText
    {
        get => _inputText;
        set
        {
            _inputText = value;
            RaisePropertyChanged();
            SubmitCommand.RaiseCanExecuteChanged();
        }
    }

    public string? OutputText
    {
        get => _outputText;
        set
        {
            _outputText = value;
            RaisePropertyChanged();
        }
    }

    public bool IsLoading
    {
        get => _isLoading;
        set
        {
            _isLoading = value;
            RaisePropertyChanged();
            SubmitCommand.RaiseCanExecuteChanged();
        }
    }

    private bool CanSubmit(object? arg) => !string.IsNullOrEmpty(InputText) && !IsLoading;

    private async void Submit(object? obj)
    {
        try
        {
            IsLoading = true;
            var settings = _settingsRepository.Load();
            var analysis = await _openAiRepository.GetAnalysis(InputText, settings);
            IsLoading = false;
            OutputText = analysis;
        }
        finally
        {
            IsLoading = false;
        }
    }
}