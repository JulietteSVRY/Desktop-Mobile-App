using MediatR;

namespace RickAndMorty.Commands.GetCharacterFromApi;

public sealed record GetCharacterFromApiRequest : IRequest<GetCharacterFromApiResponse> {}