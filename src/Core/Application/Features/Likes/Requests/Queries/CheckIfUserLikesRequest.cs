using MediatR;

namespace Application.Features.Likes.Requests.Queries;

public record CheckIfUserLikesRequest(Guid UserId, Guid LikesId) : IRequest<bool>;