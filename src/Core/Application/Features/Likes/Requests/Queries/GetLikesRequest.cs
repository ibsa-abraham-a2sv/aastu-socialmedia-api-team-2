using Application.DTOs.Likes;
using MediatR;

namespace Application.Features.Likes.Requests.Queries;

public record GetLikesRequest(Guid UserId) : IRequest<List<LikesDto>>;