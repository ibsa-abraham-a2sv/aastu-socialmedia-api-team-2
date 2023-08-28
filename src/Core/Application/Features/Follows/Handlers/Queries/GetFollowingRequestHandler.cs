using Application.Contracts.Persistence;
using Application.DTOs.Follows;
using Application.Features.Follows.Requests.Queries;
using AutoMapper;
using MediatR;

namespace Application.Features.Follows.Handlers.Queries;

public class GetFollowingRequestHandler : IRequestHandler<GetFollowingRequest, List<FollowsReturnDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
        
    public GetFollowingRequestHandler(IUnitOfWork unitOfWork, IMapper mapper) {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<List<FollowsReturnDto>> Handle(GetFollowingRequest request, CancellationToken cancellationToken)
    {
        var results = await _unitOfWork.FollowsRepository.GetFollowing(request.Id);

        return _mapper.Map<List<FollowsReturnDto>>(results);
    }
}