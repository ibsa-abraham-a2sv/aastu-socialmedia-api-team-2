using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Post
{
    public class DeletePostDto
    {
            public Guid UserId { get; set; } 
           public Guid PostId { get; set; } 

    }
}