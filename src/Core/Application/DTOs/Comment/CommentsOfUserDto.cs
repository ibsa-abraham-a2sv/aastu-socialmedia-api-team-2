using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.DTOs.Comment
{
    public class CommentsOfUserDto
    {
        public Guid Id { get; set; }
        public Guid PostId { get; set; }
        public required string Content { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}