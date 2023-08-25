using Application.Contracts.Persistence;
using Application.DTOs.Follows;
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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetNotificationsRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<List<NotificationDto>> Handle(GetNotificationsRequest request, CancellationToken cancellationToken)
        {
            var notifications = await _unitOfWork.NotificationRepository.GetNotifications(request.UserId);
            return _mapper.Map<List<NotificationDto>>(notifications);
        }
    }
}
