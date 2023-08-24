using Application.Contracts.Persistence;
using Application.Features.Likes.Requests.Commands;
using MediatR;

namespace Application.Features.Likes.Handlers.Commands;

public class RemoveLikeRequestHandler : IRequestHandler<RemoveLikeRequest, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    public RemoveLikeRequestHandler(IUnitOfWork unitOfWork) {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Unit> Handle(RemoveLikeRequest request, CancellationToken cancellationToken)
    {
        var response = await _unitOfWork.LikesRepository.RemoveLike(request.UserId, request.LikesId);
        
        return response;
    }
}