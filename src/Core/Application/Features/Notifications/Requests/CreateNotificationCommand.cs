using Application.DTOs.Notification;
using Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Notifications.Requests
{
    public class CreateNotificationCommand : IRequest<BaseCommandResponse>
    {
        public CreateNotificationDto? CreateNotificationDto { get; set; }
    }
}
