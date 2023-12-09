using MediatR;
using RickAndMorty.Abstractions;

namespace RickAndMorty.Commands.GetLikedStatus;

public class GetLikedStatusHandler(ILikedRepository likedRepository) : IRequestHandler<GetLikedStatusRequest, GetLikedStatusResponse>
{
    public async Task<GetLikedStatusResponse> Handle(GetLikedStatusRequest request, CancellationToken cancellationToken)
    {
        var status = await likedRepository.GetLikedStatus(request.Id, request.Type, cancellationToken);
        return new GetLikedStatusResponse
        {
            IsLaked = status
        };
    }
}