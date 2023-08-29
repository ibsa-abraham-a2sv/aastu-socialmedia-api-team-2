using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Common.Models;

namespace Domain.Comment
{
    public class Comment : EntityBase
    {
        public Guid UserId { get; set; }
        public required string Content { get; set; }
        
        public required Domain.Post.Post Post { get; set; }
        public Guid PostId { get; set; }
    }
}