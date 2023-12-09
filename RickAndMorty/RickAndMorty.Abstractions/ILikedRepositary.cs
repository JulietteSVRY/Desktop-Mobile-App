using RickAndMorty.Model.Entity;

namespace RickAndMorty.Abstractions;

public interface ILikedRepository
{
    Task<IReadOnlyCollection<Liked>> GetLiked(int? limit, int? offset, CancellationToken cancellationToken = default);
    Task<ulong> AddLiked(ulong referenceId, LikedType type, CancellationToken cancellationToken = default);
    Task DeleteLiked(ulong referenceId, LikedType type, CancellationToken cancellationToken = default);
    Task<bool> GetLikedStatus(ulong referenceId, LikedType likedType, CancellationToken cancellationToken = default);
}