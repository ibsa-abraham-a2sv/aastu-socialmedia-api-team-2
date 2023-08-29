using Application.DTOs.Notification;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Notifications.Requests
{
    public class GetNotificationDetailRequest : IRequest<NotificationDto>
    {
        public Guid UserId { get; set; }
        public Guid NotificationId { get; set; }
    }
}
