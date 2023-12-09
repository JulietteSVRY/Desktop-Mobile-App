using MediatR;
using RickAndMorty.Abstractions.HttpClients;

namespace RickAndMorty.Commands.GetLocationsFromApi;

public sealed class GetLocationsFromApiHandler : IRequestHandler<GetLocationsFromApiRequest, GetLocationsFromApiResponse>
{
    private readonly IRickAndMortyHttpClient _httpClient;

    public GetLocationsFromApiHandler(IRickAndMortyHttpClient httpClient) => 
        _httpClient = httpClient;

    public async Task<GetLocationsFromApiResponse> Handle(GetLocationsFromApiRequest request, CancellationToken cancellationToken)
    {
        var locations = await _httpClient.GetLocations(cancellationToken);
        return new GetLocationsFromApiResponse
        {
            Locations = locations
        };
    }
}