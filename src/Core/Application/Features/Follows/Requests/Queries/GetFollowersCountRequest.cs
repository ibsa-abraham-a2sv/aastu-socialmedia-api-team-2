using MediatR;

namespace Application.Features.Follows.Requests.Queries;

public record GetFollowersCountRequest(Guid UserId) : IRequest<int>;