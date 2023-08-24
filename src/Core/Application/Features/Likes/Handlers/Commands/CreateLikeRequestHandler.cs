using Application.Contracts.Persistence;
using Application.Features.Likes.Requests.Commands;
using MediatR;

namespace Application.Features.Likes.Handlers.Commands;

public class CreateLikeRequestHandler : IRequestHandler<CreateLikeRequest, Guid?>
{
    private readonly IUnitOfWork _unitOfWork;
    public CreateLikeRequestHandler(IUnitOfWork unitOfWork) {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Guid?> Handle(CreateLikeRequest request, CancellationToken cancellationToken)
    {
        var response = await _unitOfWork.LikesRepository.CreateLike(request.UserId, request.LikesId);

        return response;
    }
}