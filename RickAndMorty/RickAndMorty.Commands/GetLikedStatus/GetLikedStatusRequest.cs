using MediatR;
using RickAndMorty.Model.Entity;

namespace RickAndMorty.Commands.GetLikedStatus;

public sealed record GetLikedStatusRequest : IRequest<GetLikedStatusResponse>
{
    public required ulong Id { get; init; }
    public required LikedType Type { get; init; }
}