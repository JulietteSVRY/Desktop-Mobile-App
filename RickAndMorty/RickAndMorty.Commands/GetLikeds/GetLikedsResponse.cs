using RickAndMorty.Model.RickAndMortyApiJsonObjects;

namespace RickAndMorty.Commands.GetLikeds;

public sealed record GetLikedsResponse
{
    public required SimpleResultCharacter[] Characters { get; init; } = Array.Empty<SimpleResultCharacter>();
    public required SimpleResultLocation[] Locations { get; init; } = Array.Empty<SimpleResultLocation>();
}