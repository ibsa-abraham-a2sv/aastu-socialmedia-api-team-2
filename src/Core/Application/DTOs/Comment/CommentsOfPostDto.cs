using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.DTOs.Comment
{
    public class CommentsOfPostDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public required string Content { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}