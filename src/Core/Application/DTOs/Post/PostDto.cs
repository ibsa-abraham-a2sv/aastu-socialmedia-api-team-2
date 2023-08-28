using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.DTOs.Post
{
    public class PostDto
    {
        public Guid Id { get; set; } 
        public Guid UserId { get; set; } 
        public string Content { get; set; }
            

    }
}

