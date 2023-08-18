using SocialSync.Core.Domain.Common.Models;

namespace SocialSync.Core.Domain.Follows;

public sealed class Follows : EntityBase {
    public Guid UserId { get; private set; }
    public Guid FollowsId { get; private set; }
    public DateTime StartDate { get; private set; }
}
