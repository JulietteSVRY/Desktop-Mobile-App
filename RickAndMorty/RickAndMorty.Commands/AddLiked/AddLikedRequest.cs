using MediatR;
using RickAndMorty.Model.Entity;

namespace RickAndMorty.Commands.AddLiked;

public sealed record AddLikedRequest : IRequest<AddLikedResponse>
{
    public required ulong ReferenceId { get; init; }
    public required LikedType LikedType { get; init; }
}