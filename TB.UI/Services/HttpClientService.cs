using Newtonsoft.Json;
using System.Text;
using TB.Shared.Dto.Global;

namespace TB.UI.Services
{
    public class HttpClientService : IHttpClientService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerSettings _jsonSerializerSetting;
        public HttpClientService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _jsonSerializerSetting = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
            };
        }

        public async Task<ResponseDto<TResult>> GetAsync<TResult>(string url)
        {
            var response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                var deserialize = JsonConvert.DeserializeObject<ResponseDto<TResult>>(result);
                if (deserialize != null)
                {
                    return deserialize;
                }
            }
            return new ResponseDto<TResult>(false, "سیستم با خطا مواجه شد", default(TResult));
        }
        public async Task<ResponseDto<TResult>> PostAsync<TResult, TData>(string url, TData data)
        {
            var serializeData = JsonConvert.SerializeObject(data, _jsonSerializerSetting);
            var stringContent = new StringContent(serializeData, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(url, stringContent);
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                var deserialize = JsonConvert.DeserializeObject<ResponseDto<TResult>>(result, _jsonSerializerSetting);
                if (deserialize != null)
                {
                    return deserialize;
                }
            }
            return new ResponseDto<TResult>(false, "سیستم با خطا مواجه شد", default(TResult));
        }
    }
}
