using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Post
{
    public class UpdatePostDto
    {
        public Guid Id { get; set; } 
        public Guid UserId { get; set; } 
        public string Content { get; set; }
    }
}
