using Domain.Follows;
using MediatR;

namespace Application.Contracts.Persistence;

public interface IFollowsRepository : IGenericRepository<Follows>
{
    Task<bool> CheckIfUserFollows(Guid userId, Guid followsId);
    Task<Guid?> Follow(Guid userId, Guid followsId);
    Task<Unit> Unfollow(Guid userId, Guid followsId);

    Task<List<Follows>> GetFollowers(Guid userId);
    Task<List<Follows>> GetFollowing(Guid userId);
}
