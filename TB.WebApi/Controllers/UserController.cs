using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TB.Application.Interfaces;
using TB.Domain.Models;
using TB.Shared.Dto.Global;
using TB.Shared.Dto.User;
using TB.WebApi.Helper;
using TB.WebApi.Services;

namespace TB.WebApi.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IFileService _fileService;
        public UserController(IMapper mapper, IUserService userService,IFileService fileService)
        {
            _mapper = mapper;
            _userService = userService;
            _fileService = fileService;
        }

        [HttpPost("create")]
        public ResponseDto<bool> Create([FromBody] UserDto data)
        {
            User model = _mapper.Map<UserDto, User>(data);

            try
            {
                if (data.File != null)
                {
                    model.Image = _fileService.Save(data.File , nameof(TB.Domain.Models.User));
                }
                model.CreatedAt = DateTime.UtcNow;
                model.Password = HashPassword.Generate(data.Password);

                bool result = _userService.Create(model).GetAwaiter().GetResult();

                return new ResponseDto<bool>(true, "ذخیره سازی با موفقیت انجام شد", result);
            }
            catch (Exception e)
            {
                return new ResponseDto<bool>(false, e.Message, false);
            }
        }
        [HttpPost("update")]
        public ResponseDto<bool> Update([FromBody] UserDto data)
        {
            User model = _mapper.Map<UserDto, User>(data);

            try
            {
                if (data.File != null)
                {
                    if (!string.IsNullOrEmpty(data.Image))
                    {
                        _fileService.Delete(data.Image); // User/dasdas.jpg
                    }
                    model.Image = _fileService.Save(data.File, nameof(TB.Domain.Models.User));
                }
                //model.CreatedAt = DateTime.UtcNow;
                if (!string.IsNullOrEmpty(model.Password))
                {
                    model.Password = HashPassword.Generate(data.Password);
                }

                bool result = _userService.Update(model).GetAwaiter().GetResult();

                return new ResponseDto<bool>(true, "ذخیره سازی با موفقیت انجام شد", result);
            }
            catch (Exception e)
            {
                return new ResponseDto<bool>(false, e.Message, false);
            }
        }
        [HttpPost("delete")]
        [Authorize(Roles = "Admin")]
        public ResponseDto<bool> Delete([FromBody] int id)
        {
            try
            {
                bool result = _userService.Delete(id).GetAwaiter().GetResult();

                return new ResponseDto<bool>(true, "حذف با موفقیت انجام شد", result);
            }
            catch (Exception e)
            {
                return new ResponseDto<bool>(false, e.Message, false);
            }
        }
        [HttpGet("getAll")]
        public ResponseDto<List<UserDto>> GetAll()
        {
            try
            {
                List<User> result = _userService.GetAll().GetAwaiter().GetResult();
                List<UserDto> users = _mapper.Map<List<User>, List<UserDto>>(result);

                return new ResponseDto<List<UserDto>>(true, "با موفقیت دریافت شد", users);

            }
            catch (Exception e)
            {
                return new ResponseDto<List<UserDto>>(false, e.StackTrace, new List<UserDto>());
            }
        }
        [HttpPost("findById")]
        public ResponseDto<UserDto> FindById([FromBody] int id)
        {
            try
            {
                User result = _userService.FindById(id).GetAwaiter().GetResult();
                UserDto data = _mapper.Map<User, UserDto>(result);

                return new ResponseDto<UserDto>(true, "با موفقیت دریافت شد", data);

            }
            catch (Exception e)
            {
                return new ResponseDto<UserDto>(false, e.StackTrace, new UserDto());
            }
        }
    }
}
