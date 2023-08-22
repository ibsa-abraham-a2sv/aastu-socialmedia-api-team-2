using Domain.Common.Models;

namespace Domain.Post
{
    public sealed class Post : EntityBase
    {
        public Guid UserId { get; set; } 
        public string Content { get; set; }
    
    }
}
