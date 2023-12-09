using MediatR;
using RickAndMorty.Abstractions;

namespace RickAndMorty.Commands.DeleteLiked;

public class DeleteLikedHandler(ILikedRepository likedRepository) : IRequestHandler<DeleteLikedRequest, DeleteLikedResponse>
{
    public async Task<DeleteLikedResponse> Handle(DeleteLikedRequest request, CancellationToken cancellationToken)
    {
        await likedRepository.DeleteLiked(request.ReferenceId, request.LikedType, cancellationToken);
        return new DeleteLikedResponse();
    }
}