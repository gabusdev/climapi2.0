using AutoMapper;
using Climapi.Common.DTO.Request;
using Climapi.Common.DTO.Response;
using Climapi.Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace Climapi.Services.Utils
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<RegisterDto, User>();
            CreateMap<User, UserDto>();
        }
    }
}
