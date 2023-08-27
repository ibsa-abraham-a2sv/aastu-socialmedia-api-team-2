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

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public CreateNotificationRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<BaseCommandResponse> Handle(CreateNotificationCommand request, CancellationToken cancellationToken)
        {
            var notification = _mapper.Map<Notification>(request.CreateNotificationDto);
            var response = await _unitOfWork.NotificationRepository.Add(notification);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new BaseCommandResponse() { Id = notification.Id, Success = true, Message = "Successfully added the notification request" };
           
        }

    }
}
