using MediatR;

namespace Application.Features.Follows.Requests.Queries;

public record CheckIfUserFollowsRequest(Guid UserId, Guid FollowsId) : IRequest<bool>;