using TB.Shared.Dto.Global;
using TB.Shared.Dto.User;

namespace TB.UI.Services.Repository
{
    public class UserService: IUserService
    {
        public IHttpClientService _http { get; set; }
        string baseUrl = "/api/users";
        public UserService(IHttpClientService http)
        {
            _http = http;
        }

        public async Task<ResponseDto<bool>> Create(UserDto data)
        {
            return await _http.PostAsync<bool, UserDto>($"{baseUrl}/create", data);
        }
        public async Task<ResponseDto<bool>> Update(UserDto data)
        {
            return await _http.PostAsync<bool, UserDto>($"{baseUrl}/update", data);
        }
        public async Task<ResponseDto<List<UserDto>>> GetAll()
        {
            return await _http.GetAsync<List<UserDto>>($"{baseUrl}/getAll");
        }
        public async Task<ResponseDto<UserDto>> FindById(int id)
        {
            return await _http.PostAsync<UserDto, int>($"{baseUrl}/findById", id);
        }
        public async Task<ResponseDto<bool>> Delete(int id)
        {
            return await _http.PostAsync<bool, int>($"{baseUrl}/delete", id);
        }
    }
    public interface IUserService
    {
        Task<ResponseDto<bool>> Create(UserDto data);
        Task<ResponseDto<bool>> Delete(int id);
        Task<ResponseDto<UserDto>> FindById(int id);
        Task<ResponseDto<List<UserDto>>> GetAll();
        Task<ResponseDto<bool>> Update(UserDto data);
    }
}
