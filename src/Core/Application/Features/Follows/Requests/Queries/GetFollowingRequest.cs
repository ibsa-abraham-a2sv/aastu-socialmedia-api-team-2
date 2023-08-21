using MediatR;

namespace Application.Features.Follows.Requests.Queries;

public abstract record GetFollowingRequest(Guid Id) : IRequest<List<Guid>>;