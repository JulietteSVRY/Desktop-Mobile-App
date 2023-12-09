using RickAndMorty.Model.RickAndMortyApiJsonObjects;

namespace RickAndMorty.Abstractions.HttpClients;

public interface IRickAndMortyHttpClient
{
    Task<GetLocations[]> GetLocations(CancellationToken cancellationToken = default);
    Task<GetCharacter[]> GetCharacters(CancellationToken cancellationToken = default);
    Task<string?> GetLocationName(string url, CancellationToken cancellationToken = default);
    Task<SimpleResultCharacter[]> GetCharacterByIds(ulong[] ids, CancellationToken cancellationToken = default);
    Task<SimpleResultLocation[]> GetLocationByIds(ulong[] ids, CancellationToken cancellationToken = default);
}