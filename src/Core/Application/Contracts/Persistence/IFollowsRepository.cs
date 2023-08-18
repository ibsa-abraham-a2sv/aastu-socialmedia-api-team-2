using MediatR;

namespace SocialSync.Core.Application.Contracts.Persistence;

public interface IFollowsRepository {
    Task<Guid> Follow(Guid userId, Guid FollowsId);
    Task<Unit> Unfollow(Guid userId, Guid FollowsId);
}
