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
      
        
    }
}
