using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Common.Models;
using Domain.Post;

namespace Domain.Hashtag
{
    public class PostHashtag : EntityBase
    {
        public Guid PostId { get; set; }
        public Domain.Post.Post? Post { get; set; }
        
        public Guid HashtagId { get; set; }
        public Hashtag? Hashtag { get; set; }
    }
}