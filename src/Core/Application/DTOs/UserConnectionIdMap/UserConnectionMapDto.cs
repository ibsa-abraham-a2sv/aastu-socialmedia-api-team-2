using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.UserConnectionIdMap
{
    public class UserConnectionMapDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string ConnectionId { get; set; } = string.Empty;

    }
}
