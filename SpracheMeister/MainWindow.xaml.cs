using SpracheMeister.ViewModel;

namespace SpracheMeister;

public partial class MainWindow
{
    public MainWindow(MainViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}