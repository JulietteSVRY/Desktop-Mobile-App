using RickAndMorty.Model.RickAndMortyApiJsonObjects;

namespace RickAndMorty.Commands.GetCharacterFromApi;

public sealed record GetCharacterFromApiResponse
{
    public required GetCharacter[] Characters { get; init; }
}