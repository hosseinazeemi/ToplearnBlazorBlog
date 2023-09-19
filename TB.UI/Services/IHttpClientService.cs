using TB.Shared.Dto.Global;

namespace TB.UI.Services
{
    public interface IHttpClientService
    {
        Task<ResponseDto<TResult>> GetAsync<TResult>(string url);
        Task<ResponseDto<TResult>> PostAsync<TResult, TData>(string url, TData data);
    }
}
