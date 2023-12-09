using MediatR;

namespace RickAndMorty.Commands.GetLocationsFromApi;

public record GetLocationsFromApiRequest : IRequest<GetLocationsFromApiResponse>
{
    
}