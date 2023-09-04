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
using Domain.Hashtag;
using Application.DTOs.Hashtag;

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

         CreateMap<Post, PostDto>().ReverseMap();
          CreateMap<Post, UpdatePostDto>().ReverseMap();
           


       
        #region Notification
        CreateMap<Notification, NotificationDto>().ReverseMap();
        CreateMap<Notification, CreateNotificationDto>().ReverseMap();
        #endregion Notification

         


        CreateMap<Hashtag, HashtagDto>().ReverseMap();
    }
}
