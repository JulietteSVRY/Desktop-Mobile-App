using RickAndMorty.Model.Entity;

namespace RickAndMorty.Commands.GetLikeds;

public record LikedItem
{
    public required ulong ReferenceId { get; init; }
    public required LikedType Type { get; init; }
    public required bool IsLiked { get; init; }
}