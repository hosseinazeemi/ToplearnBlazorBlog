using TB.Shared.Dto.Content;
using TB.Shared.Dto.Global;

namespace TB.UI.Services.Repository
{
    public class ContentService : IContentService
    {
        public IHttpClientService _http { get; set; }
        string baseUrl = "/api/content";
        public ContentService(IHttpClientService http)
        {
            _http = http;
        }

        public async Task<ResponseDto<bool>> Create(ContentDto data)
        {
            return await _http.PostAsync<bool, ContentDto>($"{baseUrl}/create", data);
        }
        public async Task<ResponseDto<bool>> Update(ContentDto data)
        {
            return await _http.PostAsync<bool, ContentDto>($"{baseUrl}/update", data);
        }
        public async Task<ResponseDto<List<ContentDto>>> GetAll()
        {
            return await _http.GetAsync<List<ContentDto>>($"{baseUrl}/getAll");
        }
        public async Task<ResponseDto<ContentDto>> FindById(int id)
        {
            return await _http.PostAsync<ContentDto, int>($"{baseUrl}/findById", id);
        }
        public async Task<ResponseDto<bool>> Delete(int id)
        {
            return await _http.PostAsync<bool, int>($"{baseUrl}/delete", id);
        }
    }
    public interface IContentService
    {
        Task<ResponseDto<bool>> Create(ContentDto data);
        Task<ResponseDto<bool>> Delete(int id);
        Task<ResponseDto<ContentDto>> FindById(int id);
        Task<ResponseDto<List<ContentDto>>> GetAll();
        Task<ResponseDto<bool>> Update(ContentDto data);
    }
}
