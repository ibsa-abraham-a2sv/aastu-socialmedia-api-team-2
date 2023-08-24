using MediatR;

namespace Application.Features.Post.Requests.Queries;

public record GetPostsRequest() : IRequest<List<Domain.Post.Post>>;