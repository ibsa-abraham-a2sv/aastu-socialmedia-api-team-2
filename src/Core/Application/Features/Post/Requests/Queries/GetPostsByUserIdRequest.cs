using Application.DTOs.Post;
using MediatR;

namespace Application.Features.Post.Requests.Queries;

public record GetPostsByUserIdRequest(Guid userId,int pageIndex, int pageSize) : IRequest<List<PostDto>>;