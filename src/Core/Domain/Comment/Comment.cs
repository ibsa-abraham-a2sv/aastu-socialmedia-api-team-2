using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Common.Models;

namespace Domain.Comment
{
    public class Comment : EntityBase
    {
        public Guid PostId { get; set; }
        public Guid UserId { get; set; }
        // public Post
        public required string Content { get; set; }
    }
}