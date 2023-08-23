using Application.Contracts.Persistence;
using Application.Features.Likes.Requests.Queries;
using MediatR;

namespace Application.Features.Likes.Handlers.Queries;

public class CheckIfUserLikesRequestHandler : IRequestHandler<CheckIfUserLikesRequest, bool>
{
    private readonly IUnitOfWork _unitOfWork;
    public CheckIfUserLikesRequestHandler(IUnitOfWork unitOfWork) {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<bool> Handle(CheckIfUserLikesRequest request, CancellationToken cancellationToken)
    {
        return await _unitOfWork.LikesRepository.CheckIfUserLikes(request.UserId, request.LikesId);
    }
}