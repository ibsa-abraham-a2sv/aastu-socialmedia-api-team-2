using MediatR;

namespace Application.Features.Follows.Requests.Queries;

public record GetFollowingCountRequest(Guid UserId) : IRequest<int>;