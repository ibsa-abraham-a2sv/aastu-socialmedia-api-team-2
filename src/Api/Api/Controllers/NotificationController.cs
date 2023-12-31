﻿using Application.Constants;
using Application.DTOs.Notification;
using Application.Features.Notifications.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Persistence.Service;
using System;
using System.Security.Claims;
using Application.DTOs.Comment;
using Application.Features.Comments.Requests.Commands;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NotificationController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IHttpContextAccessor _contextAccessor;
        public NotificationController(IMediator mediator, IHttpContextAccessor contextAccessor)
        {
            _mediator = mediator;
            _contextAccessor = contextAccessor;
        }
        // GET: api/<NotificationController>
        [HttpGet("[action]")]
        public async Task<ActionResult<List<NotificationDto>>> Get()
        {
            var id = _contextAccessor.HttpContext!.User.FindFirstValue(CustomClaimTypes.Uid);

            var query = new GetNotificationsRequest { UserId = new Guid(id!) };
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

        [HttpPut("markAsRead")]
        public async Task<ActionResult> Put(Guid id)
        {
            var userId = _contextAccessor.HttpContext!.User.FindFirstValue(CustomClaimTypes.Uid) ?? throw new UnauthorizedAccessException("User is not authorized.");
            var command = new MarkNotificationReadCommand { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }

    }
 
}
