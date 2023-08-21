using Domain.Common.Models;

namespace Domain.Follows;

public sealed class Follows : EntityBase {
    public Guid UserId { get; private set; }
    public Guid FollowsId { get; private set; }
    public DateTime StartDate { get; private set; }
}
