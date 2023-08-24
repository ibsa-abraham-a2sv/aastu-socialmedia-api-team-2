using Application.Contracts.Persistence;
using Application.DTOs.Follows;
using Application.Features.Follows.Requests.Queries;
using AutoMapper;
using MediatR;

namespace Application.Features.Follows.Handlers.Queries;

public class GetFollowersRequestHandler : IRequestHandler<GetFollowersRequest, List<FollowsReturnDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    
    public GetFollowersRequestHandler(IUnitOfWork unitOfWork, IMapper mapper) {
        _unitOfWork = unitOfWork;
        _mapper = mapper;

    }
    public async Task<List<FollowsReturnDto>> Handle(GetFollowersRequest request, CancellationToken cancellationToken)
    {
        var response = await _unitOfWork.FollowsRepository.GetFollowers(request.Id);

        return _mapper.Map<List<FollowsReturnDto>>(response);
    }
}