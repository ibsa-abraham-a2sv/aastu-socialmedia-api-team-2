using Application.DTOs.Post;
using MediatR;

namespace Application.Features.Post.Requests.Queries;

public record GetPostsByUserIdRequest(Guid userId) : IRequest<List<Domain.Post.Post>>;