using Application.DTOs.Post;
using MediatR;

namespace Application.Features.Post.Requests.Queries;

public record GetFollowingPostsRequest(Guid userId, int pageIndex, int pageSize) : IRequest<List<Domain.Post.Post>>;