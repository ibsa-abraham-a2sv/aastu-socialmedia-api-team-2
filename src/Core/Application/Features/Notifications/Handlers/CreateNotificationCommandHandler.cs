using Application.Contracts.Persistence;
using Application.DTOs.Notification;
using Application.Features.Notifications.Requests;
using Application.Responses;
using AutoMapper;
using Domain.Notification;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Notifications.Handlers
{
    public class CreateNotificationRequestHandler : IRequestHandler<CreateNotificationCommand, BaseCommandResponse>
    {
        private readonly INotificationRepository _notificationRepository;
        //private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public CreateNotificationRequestHandler(INotificationRepository notificationRepository, IMapper mapper)
        {
            _notificationRepository = notificationRepository;
            _mapper = mapper;
            //_userRepository = userRepository;
        }
        public async Task<BaseCommandResponse> Handle(CreateNotificationCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();


            var notification = _mapper.Map<Notification>(request.CreateNotificationDto);
            notification = await _notificationRepository.Add(notification);
            response.Success = true;
            response.Message = "Creation successful";
            response.Id = notification.Id;

            

            return response;
        }

    }
}
