using Application.Contracts.Persistence;
using Application.Exceptions;
using Domain.Notification;
using Microsoft.EntityFrameworkCore;

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
            if (notification == null) throw new NotFoundException("Notification", nameof(notification));
            notification.IsRead = true;
            await _dbContext.SaveChangesAsync();
            return notification;


        }

        public async Task<List<Notification>> GetNotifications(Guid userId)
        {
            var notifications = await _dbContext.Notifications.Where(n => n.UserId == userId).ToListAsync();

            await _dbContext.SaveChangesAsync();
            return notifications;

        }
    }

}

