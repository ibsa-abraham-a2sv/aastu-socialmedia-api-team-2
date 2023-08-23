using Domain.Common.Models;

namespace Domain.Unlikes;

public class Unlikes : EntityBase
{
    public Guid UserId { get; set; }
    public Guid UnlikesId { get; set; }
}
