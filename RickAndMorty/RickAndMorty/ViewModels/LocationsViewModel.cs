using System.Collections.ObjectModel;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using RickAndMorty.Commands.GetLikedStatus;
using RickAndMorty.Commands.GetLocationsFromApi;
using RickAndMorty.Components;
using RickAndMorty.Infrastructure.Database;
using RickAndMorty.Model.Entity;

namespace RickAndMorty.ViewModels;

public partial class LocationsViewModel : ViewModelBase
{
    private readonly IMediator _mediator;
    private readonly RickAndMortyDbContext _rickAndMortyDbContext;

    [ObservableProperty] private ObservableCollection<LocationItemComponentViewModel> _locations = new();

    public LocationsViewModel()
    {
        _mediator = Helpers.GetAppServiceProvider().GetService<IMediator>()!;
        _rickAndMortyDbContext = Helpers.GetAppServiceProvider().GetService<RickAndMortyDbContext>()!;
    }

    [RelayCommand]
    private async Task GetStartLocations(CancellationToken cancellationToken)
    {
        if(Design.IsDesignMode)
            return;
        
        IsVisibleLoader = true;
        try
        {
            var getLocationsFromApiResponse = await _mediator.Send(new GetLocationsFromApiRequest(), cancellationToken);
            var results = getLocationsFromApiResponse.Locations.Select(x => x.Results).SelectMany(x => x).ToArray();
            foreach (var result in results)
            {
                var getLikedStatusResponse = await _mediator.Send(new GetLikedStatusRequest()
                {
                    Id = result.Id,
                    Type = LikedType.Location
                }, cancellationToken);
                Locations.Add(new LocationItemComponentViewModel
                {
                    LocationName = result.Name,
                    IsLiked = getLikedStatusResponse.IsLaked,
                    Id = result.Id
                });
                await Task.Delay(TimeSpan.FromMilliseconds(300), cancellationToken);
            }
        }
        finally
        {
            await Task.Delay(TimeSpan.FromMilliseconds(10), cancellationToken);
            IsVisibleLoader = false;
        }
    }
}