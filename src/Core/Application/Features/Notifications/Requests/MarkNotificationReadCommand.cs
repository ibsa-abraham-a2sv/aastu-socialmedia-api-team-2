using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Notifications.Requests
{
    public class MarkNotificationReadCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
    }
}
