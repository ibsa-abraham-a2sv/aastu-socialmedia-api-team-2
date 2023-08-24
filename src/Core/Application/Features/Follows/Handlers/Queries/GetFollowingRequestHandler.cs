using Application.Contracts.Persistence;
using Application.Features.Follows.Requests.Queries;
using MediatR;

namespace Application.Features.Follows.Handlers.Queries;

public class GetFollowingRequestHandler : IRequestHandler<GetFollowingRequest, List<Domain.Follows.Follows>>
{
    private readonly IUnitOfWork _unitOfWork;
        
    public GetFollowingRequestHandler(IUnitOfWork unitOfWork) {
        _unitOfWork = unitOfWork;
    }
    public async Task<List<Domain.Follows.Follows>> Handle(GetFollowingRequest request, CancellationToken cancellationToken)
    {
        var results = await _unitOfWork.FollowsRepository.GetFollowing(request.Id);

        return results;
    }
}