using Microsoft.AspNetCore.SignalR;

namespace Persistence.Service
{
    public class NotificationHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            //await Groups.AddToGroupAsync(Context.ConnectionId, "loggedin");

        }
        public async Task SendMessage(String message)
        {
            await Clients.All.SendAsync("Receive Message",$"{Context.ConnectionId}: {message}");
        }     
    }
}
