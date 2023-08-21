using Application.Contracts.Persistence;
using Application.Features.Follows.Requests.Commands;
using MediatR;

namespace Application.Features.Follows.Handlers.Commands;

public class RemoveFollowingRequestHandler : IRequestHandler<RemoveFollowingRequest, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    public RemoveFollowingRequestHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Unit> Handle(RemoveFollowingRequest request, CancellationToken cancellationToken)
    {
        await _unitOfWork.FollowsRepository.Unfollow(request.UserId, request.FollowsId);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}