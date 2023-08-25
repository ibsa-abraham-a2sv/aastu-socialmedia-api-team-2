using MediatR;

namespace Application.Features.Unlikes.Requests.Commands;

public record RemoveUnlikeRequest(Guid UserId, Guid UnlikesId) : IRequest<Unit>;