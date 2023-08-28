using Application.DTOs.Post;
using MediatR;

namespace Application.Features.Post.Requests.Queries;

public record GetFollowingPostsRequest(Guid userId) : IRequest<List<Domain.Post.Post>>;