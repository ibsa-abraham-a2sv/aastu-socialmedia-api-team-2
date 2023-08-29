using System.Collections.Immutable;
using Domain.Common.Models;
using Domain.Hashtag;

namespace Domain.Post;

    public class Post : EntityBase
    {
        public Post()
        {
            Comments = new HashSet<Domain.Comment.Comment>();
        }

        public Guid UserId { get; set; }
        public string Content { get; set; } = string.Empty;
        public ICollection<Domain.Comment.Comment> Comments { get; set; }

        public ICollection<PostHashtag> PostHashtags { get; set; } = new List<PostHashtag>();

    }
