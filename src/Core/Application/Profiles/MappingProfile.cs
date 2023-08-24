using Application.DTOs.Comment;
using AutoMapper;
using Domain.Comment;

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
    }   
    
}
