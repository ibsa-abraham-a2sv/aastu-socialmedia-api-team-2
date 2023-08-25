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
    public class GetNotificationsRequestHandler : IRequestHandler<GetNotificationsRequest, List<NotificationDto>>
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IMapper _mapper;
        public GetNotificationsRequestHandler(INotificationRepository notificationRepository, IMapper mapper)
        {
            _notificationRepository = notificationRepository;
            _mapper = mapper;
        }
        public async Task<List<NotificationDto>> Handle(GetNotificationsRequest request, CancellationToken cancellationToken)
        {
            var notifications = await _notificationRepository.GetNotifications(request.UserId);
            return _mapper.Map<List<NotificationDto>>(notifications);
        }
    }
}
