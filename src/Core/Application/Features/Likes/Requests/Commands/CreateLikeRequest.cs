using MediatR;

namespace Application.Features.Likes.Requests.Commands;

public record CreateLikeRequest(Guid UserId, Guid LikesId) : IRequest<Guid?>;