using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.DTOs.Hashtag
{
    public class UpdateHashtagDto : IHashtagDto
    {
        public Guid Id { get; set; }
        public required string Tag { get; set; }
    }
}