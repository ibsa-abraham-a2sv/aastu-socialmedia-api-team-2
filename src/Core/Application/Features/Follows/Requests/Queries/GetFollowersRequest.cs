using Application.DTOs.Follows;
using MediatR;

namespace Application.Features.Follows.Requests.Queries;

public record GetFollowersRequest(Guid Id): IRequest<List<FollowsReturnDto>>;