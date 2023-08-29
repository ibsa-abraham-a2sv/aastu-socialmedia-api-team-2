using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.DTOs.Hashtag
{
    public class CreateHashtagDto : IHashtagDto
    {
        public required string Tag { get; set; }
    }
}