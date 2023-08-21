using MediatR;

namespace Application.Features.Follows.Requests.Commands;

public record RemoveFollowingRequest(Guid UserId, Guid FollowsId) : IRequest<Unit>;
