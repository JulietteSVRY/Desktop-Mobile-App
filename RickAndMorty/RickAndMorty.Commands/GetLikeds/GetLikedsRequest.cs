using MediatR;

namespace RickAndMorty.Commands.GetLikeds;

public sealed record GetLikedsRequest : IRequest<GetLikedsResponse>
{
    public int? Limit { get; init; }
    public int? Skip { get; init; }
}