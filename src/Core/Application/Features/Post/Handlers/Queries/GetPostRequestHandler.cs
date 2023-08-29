using Application.Contracts.Persistence;
using Application.DTOs.Post;
using Application.Features.Post.Requests.Queries;
using AutoMapper;
using MediatR;

namespace Application.Features.Post.Handlers.Queries;

public class GetPostRequestHandler : IRequestHandler<GetPostRequest, PostDto>
{
    private readonly IUnitOfWork _unitOfWork;
    public readonly IMapper _mapper;
    public GetPostRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        
    }
    public async Task<PostDto> Handle(GetPostRequest request, CancellationToken cancellationToken)
    {
        var response = await _unitOfWork.PostRepository.GetPost(request.postId);

        return _mapper.Map<PostDto>(response);
    }
}