using Application.DTOs.Notification;
using Application.Features.Notifications.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Persistence.Service;
using System;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly IMediator _mediator;
        public NotificationController(IMediator mediator)
        {
            _mediator = mediator;
        }
        // GET: api/<NotificationController>
        [HttpGet("{userId:Guid}")]
        public async Task<ActionResult<List<NotificationDto>>> Get(Guid userId)
        {
            var query = new GetNotificationsRequest { UserId = userId };
            var notifications = await _mediator.Send(query);
            return Ok(notifications);
        }

        // GET api/<NotificationController>/5
        [HttpGet("{userId:Guid},{id:Guid}")]
        public async Task<ActionResult<NotificationDto>> Get(Guid userId, Guid id)
        {
            var query = new GetNotificationDetailRequest { UserId = userId, NotificationId = id };
            var notificaiton = await _mediator.Send(query);
            return Ok(notificaiton);
        }

        // POST api/<NotificationController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreateNotificationDto notificationDto)
        {
            var command = new CreateNotificationCommand { CreateNotificationDto = notificationDto };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

    }
    //[ApiController]
    //[Route("api/[controller]")]
    //public class NotificationController : ControllerBase
    //{
    //    private readonly INotificationService _notificationService;
    //    private readonly NotificationHub _notificationHub;

    //    public NotificationController(INotificationService notificationService)
    //    {
    //        _notificationService = notificationService;
    //        _notificationHub = new NotificationHub();
    //    }

    //    /// <summary>
    //    /// Add a notification.
    //    /// </summary>
    //    /// <param name="notificationDto">Notification data.</param>
    //    /// <returns>OK if the notification is added successfully.</returns>
    //    [HttpPost]
    //    public async Task<IActionResult> AddNotification([FromBody] NotificationDto notificationDto)
    //    {
    //        await _notificationService.AddNotificationAsync(notificationDto);

    //        // Broadcast the new notification
    //        await _hubContext.Clients.All.ReceiveMessage(notificationDto.Message);

    //        return Ok();
    //    }

    //    /// <summary>
    //    /// Send a notification to a specific user.
    //    /// </summary>
    //    /// <param name="userId">Recipient user ID.</param>
    //    /// <param name="notificationDto">Notification data.</param>
    //    /// <returns>OK if the notification is sent successfully, otherwise BadRequest.</returns>
    //    [HttpPost("{userId}")]
    //    public async Task<IActionResult> SendNotificationToUser(Guid userId, [FromBody] NotificationDto notificationDto)
    //    {
    //        var success = await _notificationService.SendNotificationToUserAsync(userId, notificationDto.Message);
    //        if (success)
    //            return Ok();
    //        else
    //            return BadRequest("Failed to send notification.");
    //    }

    //    /// <summary>
    //    /// Get notifications for a specific user.
    //    /// </summary>
    //    /// <param name="userId">User ID.</param>
    //    /// <returns>List of notifications for the user.</returns>
    //    [HttpGet("{userId}")]
    //    public async Task<IActionResult> GetNotifications(Guid userId)
    //    {
    //        var notifications = await _notificationService.GetAllNotification(userId);
    //        return Ok(notifications);
    //    }

    //    [HttpGet("getall")]
    //    /// <summary>
    //    /// Get all existing notifications.
    //    /// </summary>
    //    /// <returns>List of all existing notifications.</returns>
    //    public async Task<IActionResult> GetAllExistingNotifications()
    //    {
    //        var notifications = await _notificationService.GetAllExistingNotifications();
    //        return Ok(notifications);
    //    }
    //}
}
