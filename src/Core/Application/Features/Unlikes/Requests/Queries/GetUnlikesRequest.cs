using Application.DTOs.Likes;
using MediatR;

namespace Application.Features.Unlikes.Requests.Queries;

public record GetUnlikesCountRequest(Guid UnlikesId) : IRequest<int>;