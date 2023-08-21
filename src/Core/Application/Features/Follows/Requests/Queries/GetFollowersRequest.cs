using MediatR;

namespace Application.Features.Follows.Requests.Queries;

public record GetFollowersRequest(Guid Id): IRequest<List<Domain.Follows.Follows>>;