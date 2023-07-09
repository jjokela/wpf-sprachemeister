using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using SpracheMeister.Repository;
using SpracheMeister.ViewModel;

namespace SpracheMeister;

public partial class App
{
    private readonly ServiceProvider _serviceProvider;

    public App()
    {
        ServiceCollection services = new();
        ConfigureServices(services);
        _serviceProvider = services.BuildServiceProvider();
    }

    private void ConfigureServices(ServiceCollection services)
    {
        services.AddTransient<MainWindow>();
        
        services.AddTransient<MainViewModel>();
        services.AddTransient<TranslatorViewModel>();
        services.AddTransient<SettingsViewModel>();
        
        services.AddTransient<ISettingsRepository, SettingsRepository>();
        services.AddTransient<IOpenAiRepository, OpenAiRepository>();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        var mainWindow = _serviceProvider.GetService<MainWindow>();
        mainWindow?.Show();
    }
}