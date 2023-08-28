using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Common.Models;

namespace Domain.Hashtag
{
    public class PostHashtag : EntityBase
    {
        public Guid PostId { get; set; }
        // public Post? Post { get; set; }
        
        public Guid HashtagId { get; set; }
        public Hashtag? Hashtag { get; set; }
    }
}