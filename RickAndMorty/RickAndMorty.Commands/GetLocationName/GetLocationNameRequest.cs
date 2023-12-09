using MediatR;

namespace RickAndMorty.Commands.GetLocationName;

public sealed record GetLocationNameRequest : IRequest<string?>
{
    public required string Url { get; init; }
}