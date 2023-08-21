using Application.DTOs.User;
using AutoMapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, BasicUserInfoDto>().ReverseMap();
            CreateMap<User, CreateUserProfileDto>().ReverseMap();
            CreateMap<User, UpdateUserProfileDto>().ReverseMap();
        }
    }
}
