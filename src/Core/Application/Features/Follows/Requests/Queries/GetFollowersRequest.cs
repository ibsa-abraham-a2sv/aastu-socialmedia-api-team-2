using MediatR;

namespace Application.Features.Follows.Requests.Queries;

public abstract record GetFollowersRequest(Guid Id): IRequest<List<Domain.Follows.Follows>>;