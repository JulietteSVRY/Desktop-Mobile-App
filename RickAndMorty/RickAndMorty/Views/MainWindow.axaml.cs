using Avalonia.Controls;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RickAndMorty.Infrastructure.Database;

namespace RickAndMorty.Views;

public partial class MainWindow : Window
{
    private readonly RickAndMortyDbContext _rickAndMortyDbContext;
    
    public MainWindow()
    {
        InitializeComponent();
        _rickAndMortyDbContext = Helpers.GetAppServiceProvider().GetService<RickAndMortyDbContext>()!;
    }

    private async void App_OnClosing(object? sender, WindowClosingEventArgs e)
    {
        var users = await _rickAndMortyDbContext.Users.ToArrayAsync();
        foreach (var user in users)
        {
            user.IsLogin = false;
        }

        await _rickAndMortyDbContext.SaveChangesAsync();
    }
}