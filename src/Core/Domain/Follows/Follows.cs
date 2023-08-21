using Domain.Common.Models;

namespace Domain.Follows;

public sealed class Follows : EntityBase {
    public Guid UserId { get; set; }
    public Guid FollowsId { get; set; }
}
