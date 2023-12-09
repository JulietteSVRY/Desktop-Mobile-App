using System.Collections.ObjectModel;
using System.Collections.Specialized;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using RickAndMorty.Commands.AddLiked;
using RickAndMorty.Commands.DeleteLiked;
using RickAndMorty.Commands.GetLikeds;
using RickAndMorty.Components;
using RickAndMorty.Model.Entity;

namespace RickAndMorty.ViewModels;

public partial class LikedPageViewModel : ViewModelBase
{
    private static LikedPageViewModel? _instance;
    private LikedPageViewModel()
    {
        _mediator = Helpers.GetAppServiceProvider().GetService<IMediator>()!;
    }

    private readonly IMediator _mediator;

    private async void LikedPersonsOnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        switch (e.Action)
        {
            case NotifyCollectionChangedAction.Add:
                var newItem = e.NewItems![0] as PersonCardComponentViewModel;
                await _mediator.Send(new AddLikedRequest
                {
                    ReferenceId = newItem!.Id,
                    LikedType = LikedType.Person
                });
                break;
            case NotifyCollectionChangedAction.Remove:
                var olItem = e.OldItems![0] as PersonCardComponentViewModel;
                await _mediator.Send(new DeleteLikedRequest
                {
                    ReferenceId = olItem!.Id,
                    LikedType = LikedType.Person
                });
                break;
            case NotifyCollectionChangedAction.Replace:
            case NotifyCollectionChangedAction.Move:
            case NotifyCollectionChangedAction.Reset:
                break;
            default:
                throw new ArgumentOutOfRangeException("Не известный тип в NotifyCollectionChangedEventArgs.Action");
        }
    }

    private async void LikedLocationOnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        switch (e.Action)
        {
            case NotifyCollectionChangedAction.Add:
                var newItem = e.NewItems![0] as LocationItemComponentViewModel;
                await _mediator.Send(new AddLikedRequest
                {
                    ReferenceId = newItem!.Id,
                    LikedType = LikedType.Location
                });
                break;
            case NotifyCollectionChangedAction.Remove:
                var olItem = e.OldItems![0] as LocationItemComponentViewModel;
                await _mediator.Send(new DeleteLikedRequest
                {
                    ReferenceId = olItem!.Id,
                    LikedType = LikedType.Location
                });
                break;
            case NotifyCollectionChangedAction.Replace:
            case NotifyCollectionChangedAction.Move:
            case NotifyCollectionChangedAction.Reset:
                break;
            default:
                throw new ArgumentOutOfRangeException("Не известный тип в NotifyCollectionChangedEventArgs.Action");
        }
    }

    public static LikedPageViewModel GetInstance()
    {
        _instance ??= new LikedPageViewModel();
        return _instance;
    }

    [ObservableProperty]
    private ObservableCollection<PersonCardComponentViewModel> _likedPersons = new();
    [ObservableProperty]
    private ObservableCollection<LocationItemComponentViewModel> _likedLocation = new();

    [RelayCommand]
    private async Task GetLiked(CancellationToken cancellationToken)
    {
        IsVisibleLoader = true;
        var liked = await _mediator.Send(new GetLikedsRequest(), cancellationToken);
        var locations = new ObservableCollection<LocationItemComponentViewModel>();
        var persons = new ObservableCollection<PersonCardComponentViewModel>();
        foreach (var location in liked.Locations)
        {
            locations.Add(new LocationItemComponentViewModel
            {
                IsLiked = true,
                LocationName = location.Name,
                Id = location.Id
            });
        }
        foreach (var character in liked.Characters)
        {
            var vm = new PersonCardComponentViewModel
            {
                WebSourceImage = character.Image,
                FullName = character.Name,
                LastLocation = character.Location.Name,
                Status = character.Status,
                IsLiked = true
            };
            await vm.DownloadImageFromUrlCommand.ExecuteAsync(default!);
            persons.Add(vm);
        }
        LikedPersons = persons;
        LikedLocation = locations;
        
        LikedLocation.CollectionChanged += LikedLocationOnCollectionChanged;
        LikedPersons.CollectionChanged += LikedPersonsOnCollectionChanged;
        IsVisibleLoader = false;
    }

    [RelayCommand]
    private Task DeleteLikedPerson(PersonCardComponentViewModel personCardComponentViewModel, CancellationToken cancellationToken)
    {
        LikedPersons.Remove(personCardComponentViewModel);
        return Task.CompletedTask;
    }
    
    [RelayCommand]
    private Task DeleteLikedLocation(LocationItemComponentViewModel personCardComponentViewModel, CancellationToken cancellationToken)
    {
        LikedLocation.Remove(personCardComponentViewModel);
        return Task.CompletedTask;
    }
}