using CommunityToolkit.Mvvm.ComponentModel;

namespace RickAndMorty.ViewModels;

public abstract partial class ViewModelBase: ObservableObject
{
    [ObservableProperty]
    private bool _isVisibleLoader;
}