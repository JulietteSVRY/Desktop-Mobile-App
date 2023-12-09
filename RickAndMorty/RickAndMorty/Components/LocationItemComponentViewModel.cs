using CommunityToolkit.Mvvm.ComponentModel;
using RickAndMorty.ViewModels;

namespace RickAndMorty.Components;

public partial class LocationItemComponentViewModel : BaseLikedViewModel
{
    [ObservableProperty]
    private string _locationName;
    
    [ObservableProperty]
    private ulong _id;
}