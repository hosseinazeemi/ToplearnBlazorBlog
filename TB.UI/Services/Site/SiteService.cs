using TB.Shared.Dto.Global;
using TB.Shared.Dto.Site;

namespace TB.UI.Services.Site
{
    public class SiteService:ISiteService
    {
        public IHttpClientService _http { get; set; }
        string baseUrl = "/api/site";
        public SiteService(IHttpClientService http)
        {
            _http = http;
        }

        public async Task<ResponseDto<SiteDataDto>> GetSiteData()
        {
            return await _http.GetAsync<SiteDataDto>($"{baseUrl}/getSiteData");
        }
        public async Task<ResponseDto<SiteIndexDataDto>> GetIndexData()
        {
            return await _http.GetAsync<SiteIndexDataDto>($"{baseUrl}/getIndexData");
        }
    }

    public interface ISiteService
    {
        Task<ResponseDto<SiteIndexDataDto>> GetIndexData();
        Task<ResponseDto<SiteDataDto>> GetSiteData();
    }
}
