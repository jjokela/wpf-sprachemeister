using SpracheMeister.Command;

namespace SpracheMeister.ViewModel;

public class MainViewModel : ViewModelBase
{
    private ViewModelBase? _selectedViewModel;

    public TranslatorViewModel TranslatorViewModel { get; }
    public SettingsViewModel SettingsViewModel { get; }
    public DelegateCommand SelectViewModelCommand { get; }

    public MainViewModel(TranslatorViewModel translatorViewModel, SettingsViewModel settingsViewModel)
    {
        TranslatorViewModel = translatorViewModel;
        SettingsViewModel = settingsViewModel;
        SelectedViewModel = translatorViewModel;

        SelectViewModelCommand = new DelegateCommand(SelectViewModel);
    }

    public ViewModelBase? SelectedViewModel
    {
        get => _selectedViewModel;
        set
        {
            _selectedViewModel = value;
            RaisePropertyChanged();
        }
    }

    private void SelectViewModel(object? parameter)
    {
        SelectedViewModel = parameter as ViewModelBase;
    }
}