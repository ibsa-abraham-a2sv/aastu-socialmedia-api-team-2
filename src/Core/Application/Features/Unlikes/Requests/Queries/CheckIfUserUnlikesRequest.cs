using MediatR;

namespace Application.Features.Unlikes.Requests.Queries;

public record CheckIfUserUnlikesRequest(Guid UserId, Guid UnlikesId) : IRequest<bool>;