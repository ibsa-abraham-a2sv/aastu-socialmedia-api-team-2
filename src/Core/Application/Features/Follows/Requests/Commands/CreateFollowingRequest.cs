using Application.Responses;
using MediatR;

namespace Application.Features.Follows.Requests.Commands;

public record CreateFollowingRequest(Guid UserId, Guid FollowsId) : IRequest<BaseCommandResponse>;
