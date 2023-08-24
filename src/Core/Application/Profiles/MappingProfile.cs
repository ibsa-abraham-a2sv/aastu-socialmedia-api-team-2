using Application.DTOs.Follows;
using Application.DTOs.Likes;
using Application.DTOs.Unlikes;
using AutoMapper;
using Domain.Follows;
using Domain.Likes;
using Domain.Unlikes;

namespace Application.Profiles;

public class MappingProfile : Profile {
    public MappingProfile()
    {
        CreateMap<Likes, LikesDto>().ReverseMap();
        CreateMap<Unlikes, UnlikesDto>().ReverseMap();
        CreateMap<Follows, FollowsDto>().ReverseMap();

        CreateMap<Follows, FollowsReturnDto>();
    }
}
