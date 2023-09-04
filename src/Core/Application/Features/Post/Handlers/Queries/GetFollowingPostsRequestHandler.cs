using Application.Contracts.Persistence;
using Application.DTOs.Post;
using Application.Features.Post.Requests.Queries;
using AutoMapper;
using MediatR;

namespace Application.Features.Post.Handlers.Queries;

public class GetFollowingPostsRequestHandler : IRequestHandler<GetFollowingPostsRequest, List<Domain.Post.Post>>
{
    private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
    
    public GetFollowingPostsRequestHandler(IUnitOfWork unitOfWork , IMapper mapper) {
        _unitOfWork = unitOfWork;
         _mapper = mapper;
    }
    public async Task <List<Domain.Post.Post>> Handle(GetFollowingPostsRequest request, CancellationToken cancellationToken)
    {
        var response = await _unitOfWork.PostRepository.GetFollowingPosts(request.userId);
       
      return response;
    }
}