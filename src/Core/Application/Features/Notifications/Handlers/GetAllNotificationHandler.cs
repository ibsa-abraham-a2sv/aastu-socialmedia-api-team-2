using Application.Contracts.Persistence;
using Application.DTOs.Notification;
using Application.Features.Notifications.Requests;
using Domain.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Notifications.Handlers
{
    public class GetAllNotificationHandler
    {
        //private readonly INotificationRepository _notificationRepository;

        //public GetAllNotificationHandler(INotificationRepository notificationRepository)
        //{
        //    _notificationRepository = notificationRepository;
        //}

        //public async Task<IEnumerable<NotificationDto>> Handle(GetAllNotificationRequest request)
        //{
        //    var notifications = await _notificationRepository.GetAllNotification(request.UserId);
        //    return notifications.Select(n => new NotificationDto
        //    {
        //        Id = n.Id,
        //        UserId = n.UserId,
        //        Message = n.Message,
        //        IsRead = n.IsRead
        //    });
        //}
    }
}
