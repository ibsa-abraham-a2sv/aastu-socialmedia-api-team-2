using Application.Contracts.Persistence;
using Application.DTOs.Post;
using Application.Features.Post.Requests.Queries;
using AutoMapper;
using MediatR;

namespace Application.Features.Post.Handlers.Queries;

public class GetFollowingPostsRequestHandler : IRequestHandler<GetFollowingPostsRequest, List<PostDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    public readonly IMapper _mapper;
    public GetFollowingPostsRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        
    }
    public async Task <List<PostDto>> Handle(GetFollowingPostsRequest request, CancellationToken cancellationToken)
    {
        var response = await _unitOfWork.PostRepository.GetFollowingPosts(request.userId);

        return _mapper.Map<List<PostDto>>(response);
    }
}