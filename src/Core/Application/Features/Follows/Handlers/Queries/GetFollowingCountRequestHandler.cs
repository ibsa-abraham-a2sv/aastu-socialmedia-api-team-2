using Application.Contracts.Persistence;
using Application.Features.Follows.Requests.Queries;
using MediatR;

namespace Application.Features.Follows.Handlers.Queries;

public class GetFollowingCountRequestHandler : IRequestHandler<GetFollowingCountRequest, int>
{
    private readonly IUnitOfWork _unitOfWork;
        
    public GetFollowingCountRequestHandler(IFollowsRepository followsRepository, IUnitOfWork unitOfWork) {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<int> Handle(GetFollowingCountRequest request, CancellationToken cancellationToken)
    {
        return await _unitOfWork.FollowsRepository.GetFollowingCount(request.UserId);
    }
}
