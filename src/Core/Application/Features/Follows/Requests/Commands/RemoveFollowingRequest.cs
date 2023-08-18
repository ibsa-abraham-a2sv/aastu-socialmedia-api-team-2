using MediatR;

namespace SocialSync.Core.Application.Features.Follows.Requests.Commands;

public sealed record RemoveFollowingRequest(Guid UserId, Guid FollowsId) : IRequest<Unit>;
