using Application.Contracts.Persistence;
using Application.Features.Post.Requests.Queries;
using MediatR;

namespace Application.Features.Post.Handlers.Queries;

public class GetPostRequestHandler : IRequestHandler<GetPostRequest, Domain.Post.Post>
{
    private readonly IUnitOfWork _unitOfWork;
    
    public GetPostRequestHandler(IUnitOfWork unitOfWork) {
        _unitOfWork = unitOfWork;
    }
    public async Task<Domain.Post.Post> Handle(GetPostRequest request, CancellationToken cancellationToken)
    {
        var response = await _unitOfWork.PostRepository.GetPost(request.postId);

        return response;
    }
}