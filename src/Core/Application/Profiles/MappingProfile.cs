using Application.DTOs.Comment;
using AutoMapper;
using Domain.Comment;
using Application.DTOs.Follows;
using Application.DTOs.Likes;
using Application.DTOs.Unlikes;
using Domain.Follows;
using Domain.Likes;
using Domain.Unlikes;

namespace Application.Profiles;

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
    }   
    
}
