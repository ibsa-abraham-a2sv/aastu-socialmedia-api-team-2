using Domain.Unlikes;
using MediatR;

namespace Application.Contracts.Persistence;

public interface IUnlikesRepository : IGenericRepository<Unlikes>
{
    Task<bool> CheckIfUserUnlikes(Guid userId, Guid unlikesId);
    Task<Guid?> CreateUnlike(Guid userId, Guid unlikesId);
    Task<Unit> RemoveUnlike(Guid userId, Guid unlikesId);

    Task<int> GetUnlikeCounts(Guid unlikesId);
}