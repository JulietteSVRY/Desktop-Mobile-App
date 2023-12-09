using MediatR;
using RickAndMorty.Model.Entity;

namespace RickAndMorty.Commands.DeleteLiked;

public record DeleteLikedRequest : IRequest<DeleteLikedResponse>
{
    public required ulong ReferenceId { get; init; }
    public required LikedType LikedType { get; init; }
}