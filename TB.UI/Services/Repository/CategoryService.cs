using TB.Shared.Dto.Category;
using TB.Shared.Dto.Global;

namespace TB.UI.Services.Repository
{
    public class CategoryService:ICategoryService
    {
        public IHttpClientService _http { get; set; }
        string baseUrl = "/api/categories";
        public CategoryService(IHttpClientService http)
        {
            _http = http;
        }

        public async Task<ResponseDto<bool>> Create(CategoryDto data)
        {
            return await _http.PostAsync<bool, CategoryDto>($"{baseUrl}/create", data);
        }
        public async Task<ResponseDto<bool>> Update(CategoryDto data)
        {
            return await _http.PostAsync<bool, CategoryDto>($"{baseUrl}/update", data);
        }
        public async Task<ResponseDto<List<CategoryDto>>> GetAll()
        {
            return await _http.GetAsync<List<CategoryDto>>($"{baseUrl}/getAll");
        }
        public async Task<ResponseDto<CategoryDto>> FindById(int id)
        {
            //return await _http.GetAsync<CategoryDto>($"{baseUrl}/findById?id={id}");
            return await _http.PostAsync<CategoryDto, int>($"{baseUrl}/findById", id);
        }
        public async Task<ResponseDto<bool>> Delete(int id)
        {
            return await _http.PostAsync<bool, int>($"{baseUrl}/delete", id);
        }
    }

    public interface ICategoryService
    {
        Task<ResponseDto<bool>> Create(CategoryDto data);
        Task<ResponseDto<bool>> Delete(int id);
        Task<ResponseDto<CategoryDto>> FindById(int id);
        Task<ResponseDto<List<CategoryDto>>> GetAll();
        Task<ResponseDto<bool>> Update(CategoryDto data);
    }
}
