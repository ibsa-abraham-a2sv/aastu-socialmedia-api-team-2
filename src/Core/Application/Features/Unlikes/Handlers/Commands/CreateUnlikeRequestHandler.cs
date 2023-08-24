using Application.Contracts.Persistence;
using Application.Features.Unlikes.Requests.Commands;
using MediatR;

namespace Application.Features.Unlikes.Handlers.Commands;

public class CreateUnlikeRequestHandler : IRequestHandler<CreateUnlikeRequest, Guid?>
{
    private readonly IUnitOfWork _unitOfWork;
    public CreateUnlikeRequestHandler(IUnitOfWork unitOfWork) {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Guid?> Handle(CreateUnlikeRequest request, CancellationToken cancellationToken)
    {
        var response = await _unitOfWork.UnlikesRepository.CreateUnlike(request.UserId, request.UnlikesId);

        return response;
    }
}