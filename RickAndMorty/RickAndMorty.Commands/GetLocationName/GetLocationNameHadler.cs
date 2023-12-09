using MediatR;
using RickAndMorty.Abstractions.HttpClients;

namespace RickAndMorty.Commands.GetLocationName;

public sealed class GetLocationNameHandler : IRequestHandler<GetLocationNameRequest,string?>
{
    private readonly IRickAndMortyHttpClient _httpClient;

    public GetLocationNameHandler(IRickAndMortyHttpClient httpClient) => 
        _httpClient = httpClient;

    public async Task<string?> Handle(GetLocationNameRequest request, CancellationToken cancellationToken)
    {
        var locationName = await _httpClient.GetLocationName(request.Url, cancellationToken);
        return locationName;
    }
}