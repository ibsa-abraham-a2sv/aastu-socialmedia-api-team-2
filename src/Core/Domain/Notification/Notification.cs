using Domain.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Notification
{
    public class Notification : EntityBase
    {
        public Guid UserId { get; set; }
        public string Message { get; set; }
        public bool IsRead { get; set; }
                                          
    }
}
