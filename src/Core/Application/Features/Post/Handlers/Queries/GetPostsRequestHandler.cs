using Application.Contracts.Persistence;
using Application.DTOs.Post;
using Application.Features.Post.Requests.Queries;
using AutoMapper;
using MediatR;

namespace Application.Features.Post.Handlers.Queries;

public class GetPostsRequestHandler : IRequestHandler<GetPostsRequest, List<PostDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    public readonly IMapper _mapper;
    public GetPostsRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        
    }
    public async Task<List<PostDto>> Handle(GetPostsRequest request, CancellationToken cancellationToken)
    {
        var response = await _unitOfWork.PostRepository.GetPosts(request.pageIndex, request.pageSize);

        return _mapper.Map<List<PostDto>>(response);
    }
}