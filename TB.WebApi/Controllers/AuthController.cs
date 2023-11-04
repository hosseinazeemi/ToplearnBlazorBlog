using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TB.Shared.Dto.Global;
using TB.Shared.Dto.User;

namespace TB.WebApi.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        public AuthController(IConfiguration config)
        {
            _config = config;
        }

        public ResponseDto<TokenDto> Login([FromBody] LoginDto data)
        {
            // db

            UserDto user = new UserDto
            {
                Id = 100,
                Name = "ali",
                LastName = "rezaee",
                Email = "ali@gmail.com",
                Password = "yyyyyy",
                Phone = "09120002222",
                Image = "",
                Role = Shared.Enums.RoleType.Admin,
                Status = Shared.Enums.StatusType.Active,
                CreatedAt = DateTime.Now,
                Description = ""
            };

            TokenDto token = GenerateJWTToken(user);

            return new ResponseDto<TokenDto>(true , "success" , token);
        }

        private TokenDto GenerateJWTToken(UserDto user)
        {
            var claims = new List<Claim>
            {
                new Claim("Name" , user.Name),
                new Claim("LastName" , user.LastName),
                new Claim("Email" , user.Email),
                new Claim("UserId" , user.Id.ToString()),
                new Claim("Phone" , user.Phone ?? ""),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetValue<string>("JWTKey")));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.Sha256);

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
