using MediatR;
using RickAndMorty.Abstractions.HttpClients;

namespace RickAndMorty.Commands.GetCharacterFromApi;

public sealed class GetCharacterFromApiHandler : IRequestHandler<GetCharacterFromApiRequest, GetCharacterFromApiResponse>
{
    private readonly IRickAndMortyHttpClient _httpClient;

    public GetCharacterFromApiHandler(IRickAndMortyHttpClient httpClient) => 
        _httpClient = httpClient;

    public async Task<GetCharacterFromApiResponse> Handle(GetCharacterFromApiRequest request, CancellationToken cancellationToken)
    {
        var characters = await _httpClient.GetCharacters(cancellationToken);
        return new GetCharacterFromApiResponse
        {
            Characters = characters
        };
    }
}