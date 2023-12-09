using System.Security.Cryptography;
using System.Text;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RickAndMorty.Infrastructure.Database;

namespace RickAndMorty.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    private readonly RickAndMortyDbContext _rickAndMortyDbContext;
    
    [ObservableProperty]
    private string _login;

    [ObservableProperty]
    private string _password;

    [ObservableProperty]
    private bool _isLogin;

    public MainViewModel()
    {
        _rickAndMortyDbContext = Helpers.GetAppServiceProvider().GetService<RickAndMortyDbContext>()!;
      
    }

    [RelayCommand]
    private async Task LoginInApp(CancellationToken cancellationToken)
    {
        IsVisibleLoader = true;
        var login = Login;
        var user = await _rickAndMortyDbContext.Users
            .FirstOrDefaultAsync(x => x.Login == login && x.Password == Password,
                cancellationToken: cancellationToken);
        if(user is null)
            return;
        IsLogin = true;
        await _rickAndMortyDbContext.SaveChangesAsync(cancellationToken);
        IsVisibleLoader = false;
        user.IsLogin = true;
    }
}