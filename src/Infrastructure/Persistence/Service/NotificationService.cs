namespace Persistence.Service
{
    public class NotificationService : INotificationService
    {
        //private readonly INotificationRepository _notificationRepository;
        //private readonly IHubContext<NotificationHub> _hubContext;
        //private readonly IUserConnectionMapRepository _userConnectionMapRepository;

        //public NotificationService(
        //    INotificationRepository notificationRepository,
        //    IHubContext<NotificationHub> hubContext,
        //    IUserConnectionMapRepository userConnectionMapRepository)
        //{
        //    _notificationRepository = notificationRepository;
        //    _hubContext = hubContext;
        //    _userConnectionMapRepository = userConnectionMapRepository;
        //}

        //public async Task AddNotificationAsync(NotificationDto notificationDto)
        //{
        //    var notification = new NotificationDto
        //    {
        //        UserId = notificationDto.UserId,
        //        Message = notificationDto.Message
        //        // Map other properties as needed
        //    };

        //    await _notificationRepository.AddNotificationAsync(notification);
        //}


        //public async Task<IEnumerable<NotificationDto>> GetAllNotification(Guid userId)
        //{
        //    return await _notificationRepository.GetAllNotification(userId);
        //}

        //public async Task<bool> SendNotificationToUserAsync(Guid UserId, string Message)
        //{
        //    //var notification = new NotificationDto
        //    //{
        //    //    UserId = recipientId,
        //    //    Message = notificationDto.Message
        //    //    // Map other properties as needed
        //    //};

        //    //await _notificationRepository.AddNotificationAsync(notification);

        //    var connectionMapping = await _userConnectionMapRepository.GetUserConnectionMappingAsync(UserId);

        //    if (connectionMapping != null)
        //    {
        //        string connectionId = connectionMapping.ConnectionId;
        //        await _hubContext.Clients.Client(connectionId).SendAsync("ReceiveNotification", Message);
        //    }

        //    // Perform additional logic for sending the notification
        //    // For example, you could utilize external services (email, push notification, etc.)

        //    return true; // Return true if the notification was sent successfully
        //}

        //public async Task<IEnumerable<NotificationDto>> GetAllExistingNotifications()
        //{
        //    return await _notificationRepository.GetAllExistingNotifications();
        //}
    }
}
