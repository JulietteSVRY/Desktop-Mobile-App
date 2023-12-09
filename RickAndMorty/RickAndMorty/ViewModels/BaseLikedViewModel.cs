using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RickAndMorty.Components;

namespace RickAndMorty.ViewModels;

public abstract partial class BaseLikedViewModel : ViewModelBase
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ForegroundStar))]
    private bool _isLiked;
    public IBrush ForegroundStar => IsLiked ? Brushes.Gold : Brushes.White;
    
    [RelayCommand]
    private Task AddOrDeleteLocationLikedItem(LocationItemComponentViewModel locationsViewModel,CancellationToken cancellationToken)
    {
        var likedViewModel = LikedPageViewModel.GetInstance();
        if(!likedViewModel.LikedLocation.Contains(locationsViewModel))
            likedViewModel.LikedLocation.Add(locationsViewModel);
        else
            likedViewModel.LikedLocation.Remove(locationsViewModel);
        locationsViewModel.IsLiked = !locationsViewModel.IsLiked;
        return Task.CompletedTask;
    }
    
    [RelayCommand]
    private Task AddOrDeletePersonLikedItem(PersonCardComponentViewModel personsPageViewModel,CancellationToken cancellationToken)
    {
        var likedViewModel = LikedPageViewModel.GetInstance();
        if(!likedViewModel.LikedPersons.Contains(personsPageViewModel))
            likedViewModel.LikedPersons.Add(personsPageViewModel);
        else
            likedViewModel.LikedPersons.Remove(personsPageViewModel);
        personsPageViewModel.IsLiked = !personsPageViewModel.IsLiked;
        return Task.CompletedTask;
    }
}