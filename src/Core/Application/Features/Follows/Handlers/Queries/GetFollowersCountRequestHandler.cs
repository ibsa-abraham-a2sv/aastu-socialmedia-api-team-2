using Application.Contracts.Persistence;
using Application.Features.Follows.Requests.Queries;
using MediatR;

namespace Application.Features.Follows.Handlers.Queries;

public class GetFollowersCountRequestHandler : IRequestHandler<GetFollowersCountRequest, int>
{
    private readonly IUnitOfWork _unitOfWork;
        
    public GetFollowersCountRequestHandler(IUnitOfWork unitOfWork) {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<int> Handle(GetFollowersCountRequest request, CancellationToken cancellationToken)
    {
        return await _unitOfWork.FollowsRepository.GetFollowersCount(request.UserId);
    }
}