namespace Persistence.Service
{
    public interface INotificationClient
    {
        Task ReceiveMessage(String message);
    }
}
