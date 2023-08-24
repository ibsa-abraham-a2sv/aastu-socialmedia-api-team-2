using Application.Contracts.Persistence;
using Application.Features.Post.Requests.Queries;
using MediatR;

namespace Application.Features.Post.Handlers.Queries;

public class GetPostsRequestHandler : IRequestHandler<GetPostsRequest, List<Domain.Post.Post>>
{
    private readonly IUnitOfWork _unitOfWork;
    
    public GetPostsRequestHandler(IUnitOfWork unitOfWork) {
        _unitOfWork = unitOfWork;
    }
    public async Task<List<Domain.Post.Post>> Handle(GetPostsRequest request, CancellationToken cancellationToken)
    {
        var response = await _unitOfWork.PostRepository.GetPosts();

        return response;
    }
}