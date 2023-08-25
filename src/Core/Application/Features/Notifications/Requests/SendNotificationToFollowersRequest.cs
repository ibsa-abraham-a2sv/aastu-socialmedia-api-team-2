using Application.DTOs.Notification;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Notifications.Requests
{
    public class SendNotificationToFollowersRequest : IRequest<bool>
    {
        public string UserId { get; set; }
        public NotificationDto Notification { get; set; }
    }
}
