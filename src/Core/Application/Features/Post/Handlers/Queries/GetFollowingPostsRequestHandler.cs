using Application.Contracts.Persistence;
using Application.Features.Post.Requests.Queries;
using MediatR;

namespace Application.Features.Post.Handlers.Queries;

public class GetFollowingPostsRequestHandler : IRequestHandler<GetFollowingPostsRequest, List<Domain.Post.Post>>
{
    private readonly IUnitOfWork _unitOfWork;
    
    public GetFollowingPostsRequestHandler(IUnitOfWork unitOfWork) {
        _unitOfWork = unitOfWork;
    }
    public async Task <List<Domain.Post.Post>> Handle(GetFollowingPostsRequest request, CancellationToken cancellationToken)
    {
        var response = await _unitOfWork.PostRepository.GetFollowingPosts(request.userId, request.pageIndex, request.pageSize);

        return response;
    }
}