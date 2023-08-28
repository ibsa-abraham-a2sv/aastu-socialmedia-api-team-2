using Application.Contracts.Persistence;
using Application.Features.Follows.Requests.Commands;
using Application.Responses;
using MediatR;

namespace Application.Features.Follows.Handlers.Commands;

public class CreateFollowingRequestHandler : IRequestHandler<CreateFollowingRequest, BaseCommandResponse> {
    private readonly IUnitOfWork _unitOfWork;
    public CreateFollowingRequestHandler(IUnitOfWork unitOfWork) {
        _unitOfWork = unitOfWork;
    }

    public async Task<BaseCommandResponse> Handle(CreateFollowingRequest request, CancellationToken cancellationToken) {
        var response = await _unitOfWork.FollowsRepository.Follow(request.UserId, request.FollowsId);
        
        if (response == null)
            return new BaseCommandResponse() { Success = false, Message = "you can't follow this user", };
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new BaseCommandResponse(){Id = response.Value, Success = true, Message = "Successfully added the follow request"}; 
    }
}
