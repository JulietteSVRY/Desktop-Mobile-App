using Avalonia.Controls;
using RickAndMorty.ViewModels;

namespace RickAndMorty.Views;

public partial class LikedPage : UserControl
{
    public LikedPage()
    {
        InitializeComponent();
        DataContext = LikedPageViewModel.GetInstance();
    }

    private async void StyledElement_OnInitialized(object? sender, EventArgs e) => 
        await (DataContext as LikedPageViewModel)!.GetLikedCommand.ExecuteAsync(default!);
}