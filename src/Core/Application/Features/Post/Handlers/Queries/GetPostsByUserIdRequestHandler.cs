using Application.Contracts.Persistence;
using Application.DTOs.Post;
using Application.Features.Post.Requests.Queries;
using AutoMapper;
using MediatR;

namespace Application.Features.Post.Handlers.Queries;

public class GetPostsByUserIdRequestHandler : IRequestHandler<GetPostsByUserIdRequest, List<PostDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    public readonly IMapper _mapper;
    public GetPostsByUserIdRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        
    }
    public async Task<List<PostDto>> Handle(GetPostsByUserIdRequest request, CancellationToken cancellationToken)
    {
        var response = await _unitOfWork.PostRepository.GetPostsByUserId(request.userId,request.pageIndex, request.pageSize);

        return _mapper.Map<List<PostDto>>(response);
    }
}