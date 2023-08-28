using MediatR;

namespace Application.Features.Unlikes.Requests.Commands;

public record CreateUnlikeRequest(Guid UserId, Guid UnlikesId) : IRequest<Guid?>;