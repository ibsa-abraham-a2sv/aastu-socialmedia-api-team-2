using Application.Contracts.Persistence;
using Application.DTOs.Notification;
using Domain.Notification;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repository
{
    public class NotificationRepository : GenericRepository<Notification>, INotificationRepository
    {
        private readonly SocialSyncDbContext _dbContext;
        public NotificationRepository(SocialSyncDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Notification> GetNotificationDetail(Guid userId, Guid id)
        {
            //var user = await _dbContext.Users.FindAsync(userId);
            //if (user != null)
            //{
            //    var notification = await _dbContext.Notifications.FindAsync(id);

            //    if (notification != null)
            //    {
            //        notification.IsRead = true;
            //        await _dbContext.SaveChangesAsync();
            //        return notification;
            //    }
            //}
            //return null;
            var notification = await _dbContext.Notifications.FindAsync(id);
            if (notification != null)
            {
                notification.IsRead = true;
                await _dbContext.SaveChangesAsync();
                return notification;
            }
            return null;


        }

        public async Task<List<Notification>> GetNotifications(Guid userId)
        {
            //var user = await _dbContext.Users.FindAsync(userId);
            
            var notifications = await _dbContext.Notifications.Where(n => n.UserId == userId).ToListAsync();
            return notifications;
            
        }
    }
    //public class NotificationRepository : INotificationRepository
    //{
    //    private readonly List<NotificationDto> _notifications; // In-memory storage for demonstration purposes

    //    public NotificationRepository()
    //    {
    //        _notifications = new List<NotificationDto>();
    //    }

    //    public async Task AddNotificationAsync(NotificationDto notificationDto)
    //    {
    //        _notifications.Add(notificationDto);
    //        await Task.CompletedTask;
    //    }



    //    public async Task<bool> SendNotificationToUserAsync(Guid userId, string message)
    //    {
    //        var notification = new NotificationDto
    //        {
    //            UserId = userId,
    //            Message = message
    //            // Map other properties as needed
    //        };

    //        _notifications.Add(notification);

    //        // Perform additional logic for sending the notification
    //        // For example, you could utilize external services (email, push notification, etc.)

    //        return true; // Return true if the notification was sent successfully
    //    }

    //    public async Task<IEnumerable<NotificationDto>> GetAllNotification(Guid userId)
    //    {
    //        // Implement the logic to retrieve all notifications for a given user.
    //        // You can query the data store to fetch the notifications associated with the userId.
    //        // Example implementation:
    //        // var userNotifications = dbContext.Notifications.Where(n => n.UserId == userId).ToList();
    //        // return userNotifications.Select(MapNotificationEntityToDto);

    //        // Placeholder implementation
    //        await Task.Delay(100);
    //        return _notifications.Where(n => n.UserId == userId).ToList();
    //    }

    //    public async Task<IEnumerable<NotificationDto>> GetAllExistingNotifications()
    //    {
    //        // Implement the logic to retrieve all existing notifications.
    //        // Example implementation:
    //        // var allNotifications = dbContext.Notifications.ToList();
    //        // return allNotifications.Select(MapNotificationEntityToDto);

    //        // Placeholder implementation
    //        await Task.Delay(100);
    //        return _notifications.ToList();
    //    }


    //}
}

