using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;

namespace RickAndMorty;

public static class Helpers
{
    internal static IServiceProvider GetAppServiceProvider() => (Application.Current as App)!.ServiceProvider;

    internal static Control? GetMainWindow()
    {
        var applicationLifetime = (Application.Current as App)!.ApplicationLifetime;
        return applicationLifetime switch
        {
            IClassicDesktopStyleApplicationLifetime desktop => desktop.MainWindow,
            ISingleViewApplicationLifetime singleViewPlatform => singleViewPlatform.MainView,
            _ => null
        };
    }
}