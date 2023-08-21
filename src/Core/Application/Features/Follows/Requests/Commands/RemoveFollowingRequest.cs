using MediatR;

namespace Application.Features.Follows.Requests.Commands;

public abstract record RemoveFollowingRequest(Guid UserId, Guid FollowsId) : IRequest<Unit>;
