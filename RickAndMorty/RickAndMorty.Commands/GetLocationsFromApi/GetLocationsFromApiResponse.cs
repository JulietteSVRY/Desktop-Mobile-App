using RickAndMorty.Model.RickAndMortyApiJsonObjects;

namespace RickAndMorty.Commands.GetLocationsFromApi;

public sealed record GetLocationsFromApiResponse
{
    public required GetLocations[] Locations { get; init; }
}