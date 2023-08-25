using Application.DTOs.Notification;
using Domain.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.Persistence
{
    public interface INotificationRepository : IGenericRepository<Notification>
    {
        Task<List<Notification>> GetNotifications(Guid userId);
        Task<Notification> GetNotificationDetail(Guid userId, Guid id);
        //Task<bool> SendNotificationToUserAsync(Guid UserId, string Message);
        //Task AddNotificationAsync(NotificationDto notificationDto);
        //Task<IEnumerable<NotificationDto>> GetAllNotification(Guid userId);
        //Task<IEnumerable<NotificationDto>> GetAllExistingNotifications();


        //Task<bool> SendNotificationToFollowersAsync(string userId, NotificationDto notification);
        //Task<List<NotificationDto>> GetUnreadNotificationsAsync(string userId);
        //Task<bool> MarkNotificationAsReadAsync(string notificationId);
    }
}
