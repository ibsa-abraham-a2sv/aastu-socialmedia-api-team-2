using MediatR;
using SocialSync.Core.Application.Features.Follows.Requests.Commands;
using SocialSync.Core.Application.Responses;
using SocialSync.Core.Application.Contracts.Persistence;

namespace SocialSync.Core.Application.Features.Follows.Handlers.Commands;

internal sealed class CreateFollowingRequestHandler : IRequestHandler<CreateFollowingRequest, BaseCommandResponse> {
    readonly IFollowsRepository _followsRepository;
    readonly IUnitOfWork _unitOfWork;

    public CreateFollowingRequestHandler(IFollowsRepository followsRepository, IUnitOfWork unitOfWork) {
        _followsRepository = followsRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<BaseCommandResponse> Handle(CreateFollowingRequest request, CancellationToken cancellationToken) {
        await _followsRepository.Follow(request.UserId, request.FollowsId);
        
        await _unitOfWork.SaveChangesAsync();

        // have to return with the values
        return new BaseCommandResponse(){}; 
    }
}
