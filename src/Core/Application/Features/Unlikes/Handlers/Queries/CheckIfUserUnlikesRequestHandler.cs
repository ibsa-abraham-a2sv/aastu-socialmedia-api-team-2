using Application.Contracts.Persistence;
using Application.Features.Unlikes.Requests.Queries;
using MediatR;

namespace Application.Features.Unlikes.Handlers.Queries;

public class CheckIfUserUnlikesRequestHandler : IRequestHandler<CheckIfUserUnlikesRequest, bool>
{
    private readonly IUnitOfWork _unitOfWork;
    public CheckIfUserUnlikesRequestHandler (IUnitOfWork unitOfWork) {
        _unitOfWork = unitOfWork;
    }
    
    public Task<bool> Handle(CheckIfUserUnlikesRequest request, CancellationToken cancellationToken)
    {
        return _unitOfWork.UnlikesRepository.CheckIfUserUnlikes(request.UserId, request.UnlikesId);
    }
}