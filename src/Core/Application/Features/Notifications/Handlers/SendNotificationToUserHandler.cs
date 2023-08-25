using Application.Contracts.Persistence;
using Application.DTOs.Notification;
using Application.Features.Notifications.Requests;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Notifications.Handlers
{
    public class SendNotificationToUserHandler : IRequestHandler<SendNotificationToUserRequest, bool>
    {
        //private readonly INotificationRepository _notificationRepository;
        //private readonly IMapper _mapper;

        //public SendNotificationToUserHandler(INotificationRepository notificationRepository, IMapper mapper)
        //{
        //    _notificationRepository = notificationRepository;
        //    _mapper = mapper;
        //}

        //public async Task<bool> Handle(SendNotificationToUserRequest request, CancellationToken cancellationToken)
        //{
        //    var notification = _mapper.Map<NotificationDto>(request.Notification);
        //    return await _notificationRepository.SendNotificationToUserAsync(request.UserId, notification.Message);
        //}
        public Task<bool> Handle(SendNotificationToUserRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
