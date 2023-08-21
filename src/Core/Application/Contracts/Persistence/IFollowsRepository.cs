using MediatR;

namespace Application.Contracts.Persistence;

public interface IFollowsRepository {
    Task<Guid> Follow(Guid userId, Guid followsId);
    Task<Unit> Unfollow(Guid userId, Guid followsId);

    Task<List<Guid>> GetFollowers(Guid userId);
    Task<List<Guid>> GetFollowing(Guid userId);
}
