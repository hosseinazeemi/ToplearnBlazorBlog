using AutoMapper;
using TB.Domain.Models;
using TB.Shared.Dto.Category;
using TB.Shared.Dto.Comment;
using TB.Shared.Dto.Content;
using TB.Shared.Dto.User;

namespace TB.WebApi.Config
{
    public class AutoMapperConfig:Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<UserDto, User>();
            CreateMap<User, UserDto>().ForMember(p => p.Password , x => x.Ignore());
            CreateMap<CategoryDto, Category>();
            CreateMap<Category, CategoryDto>();
            CreateMap<ContentDto, Content>();
            CreateMap<Content, ContentDto>();
            CreateMap<CommentDto, Comment>();
            CreateMap<Comment, CommentDto>();
        }
    }
}
