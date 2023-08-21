using Application.Contracts.Persistence;
using Application.Features.Follows.Requests.Queries;
using MediatR;

namespace Application.Features.Follows.Handlers.Queries;

public class GetFollowersRequestHandler : IRequestHandler<GetFollowersRequest, List<Guid>>
{
    private readonly IUnitOfWork _unitOfWork;
    
    public GetFollowersRequestHandler(IFollowsRepository followsRepository, IUnitOfWork unitOfWork) {
        _unitOfWork = unitOfWork;
    }
    public async Task<List<Guid>> Handle(GetFollowersRequest request, CancellationToken cancellationToken)
    {
        var response = await _unitOfWork.FollowsRepository.GetFollowers(request.Id);

        return response;
    }
}