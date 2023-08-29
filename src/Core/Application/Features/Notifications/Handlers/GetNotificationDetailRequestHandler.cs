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
    public class GetNotificationDetailRequestHandler : IRequestHandler<GetNotificationDetailRequest, NotificationDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetNotificationDetailRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<NotificationDto> Handle(GetNotificationDetailRequest request, CancellationToken cancellationToken)
        {
            var notification = await _unitOfWork.NotificationRepository.GetNotificationDetail(request.UserId, request.NotificationId);
            return _mapper.Map<NotificationDto>(notification);
        }
    }
}
