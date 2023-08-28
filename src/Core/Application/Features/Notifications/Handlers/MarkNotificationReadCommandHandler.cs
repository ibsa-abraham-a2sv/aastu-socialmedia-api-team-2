using Application.Contracts.Persistence;
using Application.Exceptions;
using Application.Features.Notifications.Requests;
using AutoMapper;
using Domain.Comment;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Application.Features.Notifications.Handlers
{
    public class MarkNotificationReadCommandHandler : IRequestHandler<MarkNotificationReadCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public MarkNotificationReadCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(MarkNotificationReadCommand request, CancellationToken cancellationToken)
        {
            var notification = await _unitOfWork.NotificationRepository.Get(request.Id);
            if (notification is null)
                throw new NotFoundException(nameof(notification), request.Id);
            
            notification.IsRead = true;
            await _unitOfWork.NotificationRepository.Update(notification);
            await _unitOfWork.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
