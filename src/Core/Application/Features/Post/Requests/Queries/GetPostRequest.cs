using Application.DTOs.Post;
using MediatR;

namespace Application.Features.Post.Requests.Queries;

public record GetPostRequest(Guid postId) : IRequest<Domain.Post.Post>;