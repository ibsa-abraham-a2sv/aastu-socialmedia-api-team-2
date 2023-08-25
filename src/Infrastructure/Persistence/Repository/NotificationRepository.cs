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

}

