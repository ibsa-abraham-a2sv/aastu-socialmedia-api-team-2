using Application.Contracts.Persistence;
using Application.Features.Follows.Requests.Queries;
using MediatR;

namespace Application.Features.Follows.Handlers.Queries;

public class CheckIfUserFollowsRequestHandler : IRequestHandler<CheckIfUserFollowsRequest, bool>
{
    private readonly IUnitOfWork _unitOfWork;
            
    public CheckIfUserFollowsRequestHandler(IUnitOfWork unitOfWork) {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<bool> Handle(CheckIfUserFollowsRequest request, CancellationToken cancellationToken)
    {
        return await _unitOfWork.FollowsRepository.CheckIfUserFollows(request.UserId, request.FollowsId);
    }
}