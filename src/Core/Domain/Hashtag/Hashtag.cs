using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Common.Models;

namespace Domain.Hashtag
{
    public class Hashtag : EntityBase
    {
        public required string Tag { get; set; }
        
        public ICollection<PostHashtag>? PostHashtags { get; set; }
    }
}