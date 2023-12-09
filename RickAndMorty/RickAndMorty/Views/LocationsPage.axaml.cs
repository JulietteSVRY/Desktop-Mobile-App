using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using RickAndMorty.ViewModels;

namespace RickAndMorty.Views;

public partial class LocationsPage : UserControl
{
    public LocationsPage()
    {
        InitializeComponent();
#if ANDROID
        ItemsControlLocation.ItemsPanel = new FuncTemplate<Panel>(() => new VirtualizingStackPanel())!;
#endif
    }

    private async void Location_OnInitialized(object? sender, EventArgs e) => 
        await (DataContext as LocationsViewModel)!.GetStartLocationsCommand.ExecuteAsync(default!);
}