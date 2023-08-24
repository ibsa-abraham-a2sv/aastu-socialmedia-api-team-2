using Application.Contracts.Persistence;
using Application.Features.Post.Requests.Queries;
using MediatR;

namespace Application.Features.Post.Handlers.Queries;

public class GetPostsByUserIdRequestHandler : IRequestHandler<GetPostsByUserIdRequest, List<Domain.Post.Post>>
{
    private readonly IUnitOfWork _unitOfWork;
    
    public GetPostsByUserIdRequestHandler(IUnitOfWork unitOfWork) {
        _unitOfWork = unitOfWork;
    }
    public async Task<List<Domain.Post.Post>> Handle(GetPostsByUserIdRequest request, CancellationToken cancellationToken)
    {
        var response = await _unitOfWork.PostRepository.GetPostsByUserId(request.userId);

        return response;
    }
}