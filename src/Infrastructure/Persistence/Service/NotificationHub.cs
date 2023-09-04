using System.Security.Claims;
using Application.Contracts.Persistence;
using Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;

namespace Persistence.Service
{
    public class NotificationHub : Hub
    {
        // user manager
        private readonly UserManager<ApplicationUser> _userManager;
        // get the notification repository
        private readonly INotificationRepository _notificationRepository;

        public NotificationHub(UserManager<ApplicationUser> userManager, INotificationRepository notificationRepository)
        {
            _userManager = userManager;
            _notificationRepository = notificationRepository;
        }
    
        public override async Task OnConnectedAsync()
        {
            var userId = Context.User!.Claims.FirstOrDefault(c => c.Type == "uid")!.Value;
            var user = await _userManager.FindByIdAsync(userId);

            if (user != null)
            {
                user.ConnectionId = Context.ConnectionId;
                await _userManager.UpdateAsync(user);
            }

            var notifications = await _notificationRepository.GetNotifications(new Guid(userId));
            foreach (var notification in notifications) 
            {
                if(notification.IsRead == false)
                {
                    await Clients.Caller.SendAsync("ReceiveNotification", notification.Message);
                }
            }
            
            await base.OnConnectedAsync();
        }   
        public async Task SendMessage(String message)
        {
            await Clients.All.SendAsync("Receive Message",$"{Context.ConnectionId}: {message}");
        }
        
        public async Task SendNotificationToUser(string userId, string message)
        {
            if (!string.IsNullOrEmpty(userId))
            {
                await Clients.User(userId).SendAsync("ReceiveNotification", message);
            }
        }
    }
}
