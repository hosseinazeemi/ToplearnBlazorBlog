using Newtonsoft.Json;
using System.Text;
using TB.Shared.Dto.Global;
using TB.Shared.Enums;
using TB.UI.Services.AuthService;

namespace TB.UI.Services
{
    public class HttpClientService : IHttpClientService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerSettings _jsonSerializerSetting;
        private readonly JWTService _jwtService;
        public HttpClientService(HttpClient httpClient , JWTService jwtService)
        {
            _httpClient = httpClient;
            _jwtService = jwtService;
            _jsonSerializerSetting = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
            };
        }

        public async Task<ResponseDto<TResult>> GetAsync<TResult>(string url)
        {
            var auth = _jwtService.GetAuthenticationStateAsync();

            var response = await _httpClient.GetAsync(url);

            if (CheckStatusCode((int)response.StatusCode))
            {
                var result = response.Content.ReadAsStringAsync().Result;
                var deserialize = JsonConvert.DeserializeObject<ResponseDto<TResult>>(result);
                if (deserialize != null)
                {
                    return deserialize;
                }
            }

            //else
            //{
            //    throw new HttpRequestException("خطا", null, response.StatusCode);
            //}
            //try
            //{
                
            //}
            //catch (Exception e)
            //{

            //    throw;
            //}
            return new ResponseDto<TResult>(false, "سیستم با خطا مواجه شد", default(TResult));
        }
        public async Task<ResponseDto<TResult>> PostAsync<TResult, TData>(string url, TData data)
        {
            var auth = _jwtService.GetAuthenticationStateAsync();

            var serializeData = JsonConvert.SerializeObject(data, _jsonSerializerSetting);
            var stringContent = new StringContent(serializeData, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(url, stringContent);
            if (CheckStatusCode((int)response.StatusCode))
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

        private bool CheckStatusCode(int statusCode)
        {
            if (statusCode == (int)StatusCodeType.Ok)
                return true;

            return false;
            //switch (statusCode)
            //{
            //    case (int)StatusCodeType.Ok:
            //        return true;
            //        break;
            //    case (int)StatusCodeType.UnAuthorized:
            //        break;
            //    case (int)StatusCodeType.Forbidden:
            //        break;
            //    case (int)StatusCodeType.NotFound:
            //        break;
            //    case (int)StatusCodeType.ServerError:
            //        break;
            //    default:
            //        break;
            //}
        }
    }
}
