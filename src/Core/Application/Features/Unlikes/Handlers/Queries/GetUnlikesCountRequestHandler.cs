using Application.Contracts.Persistence;
using Application.Features.Unlikes.Requests.Queries;
using MediatR;

namespace Application.Features.Unlikes.Handlers.Queries;

public class GetUnlikesCountRequestHandler : IRequestHandler<GetUnlikesCountRequest, int>
{
    private readonly IUnitOfWork _unitOfWork;
    public GetUnlikesCountRequestHandler(IUnitOfWork unitOfWork) {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<int> Handle(GetUnlikesCountRequest request, CancellationToken cancellationToken)
    {
        return await _unitOfWork.UnlikesRepository.GetUnlikeCounts(request.UnlikesId);
    }
}