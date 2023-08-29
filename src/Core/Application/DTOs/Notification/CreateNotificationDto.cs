using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Notification
{
    public class CreateNotificationDto
    {
        public Guid UserId { get; set; }
        public string Message { get; set; } = "";
        public bool IsRead { get; set; } = false;
    }
}
