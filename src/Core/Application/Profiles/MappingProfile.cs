using Application.DTOs.Notification;
using Application.DTOs.UserConnectionIdMap;
using Application.Features.Notifications.Requests;
using AutoMapper;
using Domain.Notification;
using Domain.UserConnectionIdMap;

namespace Application.Profiles;

public class MappingProfile : Profile 
{
    public MappingProfile()
    {
        //CreateMap<SendNotificationToUserRequest, NotificationDto>().ReverseMap();
        CreateMap<UserConnectionMapping, UserConnectionMapDto>().ReverseMap();
        CreateMap<UserConnectionMapping, CreateUserConnectionMappingDTO>().ReverseMap();
        #region Notification
        CreateMap<Notification, NotificationDto>().ReverseMap();
        CreateMap<Notification, CreateNotificationDto>().ReverseMap();
        #endregion Notification
    }

}
