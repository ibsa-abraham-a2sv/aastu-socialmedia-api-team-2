using Application.DTOs.Follows;
using Application.DTOs.Likes;
using Application.DTOs.Unlikes;
using Application.DTOs.Notification;
using Application.DTOs.UserConnectionIdMap;
using AutoMapper;
using Domain.Follows;
using Domain.Likes;
using Domain.Unlikes;
using Domain.Notification;
using Domain.UserConnectionIdMap;

namespace Application.Profiles;

public class MappingProfile : Profile {
    public MappingProfile()
    {
        CreateMap<Likes, LikesDto>().ReverseMap();
        CreateMap<Unlikes, UnlikesDto>().ReverseMap();
        CreateMap<Follows, FollowsDto>().ReverseMap();

        CreateMap<Follows, FollowsReturnDto>().ReverseMap();

        CreateMap<UserConnectionMapping, UserConnectionMapDto>().ReverseMap();
        CreateMap<UserConnectionMapping, CreateUserConnectionMappingDTO>().ReverseMap();
       
        #region Notification
        CreateMap<Notification, NotificationDto>().ReverseMap();
        CreateMap<Notification, CreateNotificationDto>().ReverseMap();
        #endregion Notification
    }
}
