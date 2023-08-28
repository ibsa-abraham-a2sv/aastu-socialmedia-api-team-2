using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.DTOs.Comment
{
    public class CreateCommentDto : ICommentDto
    {
        public Guid PostId { get; set; }
        public required string Content { get; set; }
    }
}