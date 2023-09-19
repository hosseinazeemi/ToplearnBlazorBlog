using AutoMapper;
using TB.Domain.Models;
using TB.Shared.Dto.User;

namespace TB.WebApi.Config
{
    public class AutoMapperConfig:Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<UserDto, User>();
        }
    }
}
