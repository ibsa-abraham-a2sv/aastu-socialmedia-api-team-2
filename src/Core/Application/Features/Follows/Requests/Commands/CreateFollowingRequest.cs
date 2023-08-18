using MediatR;
using SocialSync.Core.Application.Responses;

namespace SocialSync.Core.Application.Features.Follows.Requests.Commands;

public sealed record CreateFollowingRequest(Guid UserId, Guid FollowsId) : IRequest<BaseCommandResponse>;
