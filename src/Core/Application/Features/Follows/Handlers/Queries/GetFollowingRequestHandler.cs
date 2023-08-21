using Application.Contracts.Persistence;
using Application.Features.Follows.Requests.Queries;
using MediatR;

namespace Application.Features.Follows.Handlers.Queries;

public class GetFollowingRequestHandler : IRequestHandler<GetFollowingRequest, List<Domain.Follows.Follows>>
{
    private readonly IFollowsRepository _followsRepository;
        
    public GetFollowingRequestHandler(IFollowsRepository followsRepository) {
        _followsRepository = followsRepository;
    }
    public async Task<List<Domain.Follows.Follows>> Handle(GetFollowingRequest request, CancellationToken cancellationToken)
    {
        var results = await _followsRepository.GetFollowing(request.Id);

        return results;
    }
}