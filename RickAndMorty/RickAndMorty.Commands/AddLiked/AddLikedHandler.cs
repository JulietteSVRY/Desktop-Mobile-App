using MediatR;
using RickAndMorty.Abstractions;

namespace RickAndMorty.Commands.AddLiked;

public class AddLikedHandler(ILikedRepository likedRepository) : IRequestHandler<AddLikedRequest, AddLikedResponse>
{
    public async Task<AddLikedResponse> Handle(AddLikedRequest request, CancellationToken cancellationToken)
    {
        var id = await likedRepository.AddLiked(request.ReferenceId, request.LikedType, cancellationToken);
        return new AddLikedResponse
        {
            Id = id
        };
    }
}