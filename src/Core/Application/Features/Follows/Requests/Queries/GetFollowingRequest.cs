using MediatR;

namespace Application.Features.Follows.Requests.Queries;

public record GetFollowingRequest(Guid Id) : IRequest<List<Domain.Follows.Follows>>;