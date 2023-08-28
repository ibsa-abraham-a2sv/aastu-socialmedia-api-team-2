﻿using Application.Contracts.Persistence;
using Application.DTOs.Notification;
using Application.Exceptions;
using Domain.Notification;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Exceptions;

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

            foreach (var notification in notifications)
            {
                notification.IsRead = true;
            }

            await _dbContext.SaveChangesAsync();
            return notifications;

        }
    }

}

