using TB.Shared.Dto.Global;
using TB.Shared.Dto.Setting;

namespace TB.UI.Services.Repository
{
    public class SettingService : ISettingService
    {
        public IHttpClientService _http { get; set; }
        string baseUrl = "/api/setting";
        public SettingService(IHttpClientService http)
        {
            _http = http;
        }

        public async Task<ResponseDto<bool>> SetBanner(BannerDto data)
        {
            return await _http.PostAsync<bool, BannerDto>($"{baseUrl}/banner", data);
        }
        public async Task<ResponseDto<bool>> UpdateSetting(SettingItemDto data)
        {
            return await _http.PostAsync<bool, SettingItemDto>($"{baseUrl}/updateSetting", data);
        }
        public async Task<ResponseDto<List<SettingDto>>> GetSetting()
        {
            return await _http.GetAsync<List<SettingDto>>($"{baseUrl}/getSetting");
        }
    }

    public interface ISettingService
    {
        Task<ResponseDto<List<SettingDto>>> GetSetting();
        Task<ResponseDto<bool>> SetBanner(BannerDto data);
        Task<ResponseDto<bool>> UpdateSetting(SettingItemDto data);
    }
}
