using System.Collections.ObjectModel;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using RickAndMorty.Commands.GetCharacterFromApi;
using RickAndMorty.Commands.GetLikedStatus;
using RickAndMorty.Components;
using RickAndMorty.Infrastructure.Database;
using RickAndMorty.Model.Entity;

namespace RickAndMorty.ViewModels;

public partial class PersonsPageViewModel : ViewModelBase
{
    private readonly IMediator _mediator;
    private readonly RickAndMortyDbContext _rickAndMortyDbContext;
    public PersonsPageViewModel()
    {
        _mediator = Helpers.GetAppServiceProvider().GetService<IMediator>()!;
        _rickAndMortyDbContext = Helpers.GetAppServiceProvider().GetService<RickAndMortyDbContext>()!;
    }
    [ObservableProperty]
    private ObservableCollection<PersonCardComponentViewModel> _personsPageViewModels = new();
    
    [RelayCommand]
    private async Task GetStartCharacter(CancellationToken cancellationToken)
    {
        if(Design.IsDesignMode)
            return;
        
        IsVisibleLoader = true;
        try
        {
            var getLocationsFromApiResponse = await _mediator.Send(new GetCharacterFromApiRequest(), cancellationToken);
            var characters = getLocationsFromApiResponse.Characters.Select(x => x.Results).SelectMany(x => x).ToArray();
            foreach (var result in characters)
            {
                var getLikedStatusResponse = await _mediator.Send(new GetLikedStatusRequest()
                {
                    Id = result.Id,
                    Type = LikedType.Person
                }, cancellationToken);
                PersonsPageViewModels.Add(new PersonCardComponentViewModel
                {
                    WebSourceImage = result.Image,
                    FullName = result.Name,
                    LastLocation = result.Location.Name,
                    Status = result.Status,
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