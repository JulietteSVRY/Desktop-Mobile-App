namespace RickAndMorty.Commands.GetLikedStatus;

public record GetLikedStatusResponse
{
    public required bool IsLaked { get; init; }
}