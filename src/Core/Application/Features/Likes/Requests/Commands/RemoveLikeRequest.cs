using MediatR;

namespace Application.Features.Likes.Requests.Commands;

public record RemoveLikeRequest(Guid UserId, Guid LikesId) : IRequest<Unit>;