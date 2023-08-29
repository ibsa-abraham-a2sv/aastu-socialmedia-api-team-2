using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.UserConnectionIdMap
{
    public class CreateUserConnectionMappingDTO
    {
        public Guid UserId { get; set; }
        public string ConnectionId { get; set; } = string.Empty;
    }
}
