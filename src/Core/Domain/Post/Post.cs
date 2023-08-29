using Domain.Common.Models;
using Domain.Hashtag;

namespace Domain.Post;

    public class Post : EntityBase
    {
        public Guid UserId { get; set; } 
        public required string Content { get; set; }
        
        public ICollection<PostHashtag> PostHashtags { get; set; } = new List<PostHashtag>();
    
    }
