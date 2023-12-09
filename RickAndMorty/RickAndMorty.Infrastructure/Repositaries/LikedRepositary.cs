using System.Collections.Frozen;
using Microsoft.EntityFrameworkCore;
using RickAndMorty.Abstractions;
using RickAndMorty.Infrastructure.Database;
using RickAndMorty.Model.Entity;

namespace RickAndMorty.Infrastructure.Repositaries;

public class LikedRepository(RickAndMortyDbContext dbContext) : ILikedRepository
{
    public Task<IReadOnlyCollection<Liked>> GetLiked(int? limit, int? offset, CancellationToken cancellationToken = default)
    {
        var query = dbContext.Likeds.Where(x => x.IsLiked).AsNoTracking();
        if (offset.HasValue)
            query = query.Skip(offset.Value);
        if (limit.HasValue)
            query = query.Take(limit.Value);
        return Task.FromResult<IReadOnlyCollection<Liked>>(query.ToFrozenSet());
    }
    
    

    public async Task<ulong> AddLiked(ulong referenceId, LikedType type, CancellationToken cancellationToken = default)
    {
        var dateNow = DateTimeOffset.UtcNow;
        var liked = await dbContext.Likeds.Where(x=>x.ReferenceId == referenceId && x.Type == type).SingleOrDefaultAsync(cancellationToken: cancellationToken);
        if(liked is null)
        {
            var query = await dbContext.Likeds.AddAsync(new Liked
            {
                CheatedAt = dateNow,
                ChangedAt = dateNow,
                ReferenceId = referenceId,
                Type = type,
                IsLiked = true
            }, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);
            var id = query.Entity.Id;
            return id;
        }

        liked.IsLiked = true;
        liked.ChangedAt = dateNow;
        await dbContext.SaveChangesAsync(cancellationToken);
        return liked.Id;
    }

    public async Task DeleteLiked(ulong referenceId, LikedType type, CancellationToken cancellationToken = default)
    {
        var dateNow = DateTimeOffset.UtcNow;
        var query = await dbContext.Likeds.Where(x=>x.ReferenceId == referenceId && x.Type == type).SingleOrDefaultAsync(cancellationToken: cancellationToken);
        if(query is null)
            return;
        query.IsLiked = false;
        query.ChangedAt = dateNow;
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> GetLikedStatus(ulong referenceId, LikedType likedType, CancellationToken cancellationToken = default)
    {
        var res = await dbContext.Likeds.Where(x => x.ReferenceId == referenceId && likedType == x.Type).AsNoTracking().SingleOrDefaultAsync(cancellationToken: cancellationToken);
        return res?.IsLiked ?? false;
    }
}