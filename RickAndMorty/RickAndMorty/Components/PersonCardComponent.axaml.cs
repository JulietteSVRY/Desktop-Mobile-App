using Avalonia.Controls;

namespace RickAndMorty.Components;

public partial class PersonCardComponent : UserControl
{
    public PersonCardComponent()
    {
        InitializeComponent();
    }

    private async void PersonCard_OnInitialized(object? sender, EventArgs e) => 
        await (DataContext as PersonCardComponentViewModel)!.DownloadImageFromUrlCommand.ExecuteAsync(default!);
}