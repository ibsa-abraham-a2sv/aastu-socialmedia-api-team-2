using Microsoft.AspNetCore.SignalR;

namespace Persistence.Service
{
    public class NotificationHub : Hub<INotificationClient>
    {
        public override async Task OnConnectedAsync()
        {
            await Clients.All.ReceiveMessage($"{Context.ConnectionId} has joined");

        }
        public async Task SendMessage(String message)
        {
            await Clients.All.ReceiveMessage($"{Context.ConnectionId}: {message}");
        }     
    }
}
