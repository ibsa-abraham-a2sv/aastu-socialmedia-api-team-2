using Application.Contracts.Persistence;
using Application.Features.Unlikes.Requests.Commands;
using MediatR;

namespace Application.Features.Unlikes.Handlers.Commands;

public class RemoveUnlikesRequestHandler : IRequestHandler<RemoveUnlikeRequest, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    public RemoveUnlikesRequestHandler(IUnitOfWork unitOfWork) {
        _unitOfWork = unitOfWork;
    }


    public async Task<Unit> Handle(RemoveUnlikeRequest request, CancellationToken cancellationToken)
    {
        var response = await _unitOfWork.UnlikesRepository.RemoveUnlike(request.UserId, request.UnlikesId);

        return response;
    }
}