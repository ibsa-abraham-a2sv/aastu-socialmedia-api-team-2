using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.DTOs.Comment
{
    public class UpdateCommentDto : ICommentDto
    {
        public Guid Id { get; set; }
        public required string Content { get; set; }
    }
}