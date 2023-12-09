using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TB.Application.Interfaces;
using TB.Domain.Models;
using TB.Shared.Dto.Global;
using TB.Shared.Dto.User;
using TB.WebApi.Helper;

namespace TB.WebApi.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public AuthController(IConfiguration config,IUserService userService,IMapper mapper)
        {
            _config = config;
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost("login")]
        public ResponseDto<TokenDto> Login([FromBody] LoginDto data)
        {
            var user = _userService.FindByEmail(data.Email).GetAwaiter().GetResult();
            TokenDto token;
            if (user != null && HashPassword.Verify(data.Password , user.Password))
            {
                var userData = _mapper.Map<User, UserDto>(user);
                token = GenerateJWTToken(userData);
                return new ResponseDto<TokenDto>(true, "شما با موفقیت وارد شدید", token);
            }
            else
            {
                token = new TokenDto
                {
                    Token = HashPassword.Generate("123456")
                };
                return new ResponseDto<TokenDto>(false, "اطلاعات وارد شده صحیح نیست", token);

            }
        }

        private TokenDto GenerateJWTToken(UserDto user)
        {
            var claims = new List<Claim>
            {
                new Claim("Name" , user.Name),
                new Claim("LastName" , user.LastName),
                new Claim("Email" , user.Email),
                new Claim("UserId" , user.Id.ToString()),
                new Claim("Image" , user.Image ?? ""),
                new Claim("Phone" , user.Phone ?? ""),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetValue<string>("JWTKey")));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expireDate = DateTime.UtcNow.AddMinutes(120);

            JwtSecurityToken jwt =
                new JwtSecurityToken(issuer: null, audience: null, claims, notBefore: null, expireDate, credentials);

            string result = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new TokenDto
            {
                Token = result
            };
        }
    }
}
