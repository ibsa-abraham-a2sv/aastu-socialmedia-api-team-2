using MediatR;

namespace Application.Features.Likes.Requests.Queries;

public record GetLikesCountRequest(Guid LikesId) : IRequest<int>;