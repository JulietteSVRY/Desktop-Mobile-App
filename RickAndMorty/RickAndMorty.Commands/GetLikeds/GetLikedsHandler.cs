using MediatR;
using RickAndMorty.Abstractions;
using RickAndMorty.Abstractions.HttpClients;
using RickAndMorty.Model.Entity;
using RickAndMorty.Model.RickAndMortyApiJsonObjects;

namespace RickAndMorty.Commands.GetLikeds;

public class GetLikedsHandler(ILikedRepository likedRepository, IRickAndMortyHttpClient rickAndMortyHttpClient) : IRequestHandler<GetLikedsRequest, GetLikedsResponse>
{
    public async Task<GetLikedsResponse> Handle(GetLikedsRequest request, CancellationToken cancellationToken)
    {
        var items = await likedRepository.GetLiked(request.Limit, request.Skip, cancellationToken);

        var characterIds = items.Where(x => x.Type == LikedType.Person).Select(x => x.ReferenceId).ToArray();
        var locationIds = items.Where(x => x.Type == LikedType.Location).Select(x => x.ReferenceId).ToArray();
        var persons = characterIds.Length != 0 ? await rickAndMortyHttpClient.GetCharacterByIds(characterIds, cancellationToken) : Array.Empty<SimpleResultCharacter>();
        var location = locationIds.Length != 0 ? await rickAndMortyHttpClient.GetLocationByIds(locationIds, cancellationToken)  : Array.Empty<SimpleResultLocation>();
        return new GetLikedsResponse
        {
            Characters = persons,
            Locations = location
        };
    }
}