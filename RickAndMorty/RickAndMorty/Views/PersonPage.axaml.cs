using Avalonia.Controls;
using Avalonia.Controls.Templates;
using RickAndMorty.ViewModels;

namespace RickAndMorty.Views;

public partial class PersonPage : UserControl
{
    public PersonPage()
    {
        InitializeComponent();
#if ANDROID
        ItemsControlPerson.ItemsPanel = new FuncTemplate<Panel>(() => new VirtualizingStackPanel())!;
#endif
    }

    private async void PersonPage_OnInitialized(object? sender, EventArgs e) => 
        await (DataContext as PersonsPageViewModel)!.GetStartCharacterCommand.ExecuteAsync(default!);
}