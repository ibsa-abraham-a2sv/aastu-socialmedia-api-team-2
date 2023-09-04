using Application.DTOs.Comment;
using AutoMapper;
using Domain.Comment;
using Application.DTOs.Follows;
using Application.DTOs.Likes;
using Application.DTOs.Unlikes;
using Application.DTOs.Notification;
using Domain.Notification;
using Domain.Follows;
using Domain.Likes;
using Domain.Unlikes;
namespace Application.Profiles;
using Domain.Post;
using Application.DTOs.Post;

public class MappingProfile : Profile 
{

    public MappingProfile()
    {
        #region Comment Mapping
        CreateMap<Comment, CommentDto>().ReverseMap();
        CreateMap<Comment, CommentsOfUserDto>().ReverseMap();
        CreateMap<Comment, CommentsOfPostDto>().ReverseMap();
        CreateMap<Comment, UpdateCommentDto>().ReverseMap();
        CreateMap<Comment, CreateCommentDto>().ReverseMap();
        #endregion
        
        CreateMap<Likes, LikesDto>().ReverseMap();
        CreateMap<Unlikes, UnlikesDto>().ReverseMap();
        CreateMap<Follows, FollowsDto>().ReverseMap();

        CreateMap<Follows, FollowsReturnDto>().ReverseMap();

       
        #region Notification
        CreateMap<Notification, NotificationDto>().ReverseMap();
        CreateMap<Notification, CreateNotificationDto>().ReverseMap();
        #endregion Notification

         CreateMap<Post, PostDto>()
                
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Content))
                .ReverseMap(); 
             CreateMap<Post, UpdatePostDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Content))
                .ReverseMap(); 
    }
}
