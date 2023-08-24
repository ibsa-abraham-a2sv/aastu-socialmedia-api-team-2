using Domain.Common.Models;

namespace Domain.Likes;

public class Likes : EntityBase
{
    public Guid UserId { get; set; }
    public Guid LikesId { get; set; }
}