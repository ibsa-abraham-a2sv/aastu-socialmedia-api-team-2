using Application.Contracts.Persistence;
using Application.DTOs.Likes;
using Application.Features.Likes.Requests.Queries;
using AutoMapper;
using MediatR;

namespace Application.Features.Likes.Handlers.Queries;

public class GetLikesRequestHandler : IRequestHandler<GetLikesRequest, List<LikesDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    
    public GetLikesRequestHandler(IUnitOfWork unitOfWork, IMapper mapper) {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<LikesDto>> Handle(GetLikesRequest request, CancellationToken cancellationToken)
    {
        var result = await _unitOfWork.LikesRepository.GetLikedContentList(request.UserId);

        return _mapper.Map<List<LikesDto>>(result);
    }
}