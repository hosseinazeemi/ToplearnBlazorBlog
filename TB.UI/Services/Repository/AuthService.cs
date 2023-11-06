using TB.Shared.Dto.Global;

namespace TB.UI.Services.Repository
{
    public class AuthService : IAuthService
    {
        public IHttpClientService _http { get; set; }
        string baseUrl = "/api/auth";
        public AuthService(IHttpClientService http)
        {
            _http = http;
        }

        public async Task<ResponseDto<TokenDto>> Login(LoginDto data)
        {
            return await _http.PostAsync<TokenDto, LoginDto>($"{baseUrl}/login", data);
        }
    }
    public interface IAuthService
    {
        Task<ResponseDto<TokenDto>> Login(LoginDto data);
    }
}
