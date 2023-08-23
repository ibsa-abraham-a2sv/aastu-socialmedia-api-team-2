using Application.Contracts.Persistence;
using Application.Features.Likes.Requests.Queries;
using MediatR;

namespace Application.Features.Likes.Handlers.Queries;

public class GetLikesCountRequestHandler : IRequestHandler<GetLikesCountRequest, int>
{
    private readonly IUnitOfWork _unitOfWork;
    public GetLikesCountRequestHandler(IUnitOfWork unitOfWork) {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<int> Handle(GetLikesCountRequest request, CancellationToken cancellationToken)
    {
        return await _unitOfWork.LikesRepository.GetLikesCount(request.LikesId);
    }
}