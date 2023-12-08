using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;

namespace TB.UI.Services.AuthService
{
    public class JWTService : AuthenticationStateProvider , IJWTAuthService
    {
        private readonly IJSRuntime _js;
        private readonly HttpClient _http;
        private const string _tokenKey = "token";
        private IEnumerable<Claim> _claims;
        public JWTService(IJSRuntime js, HttpClient http)
        {
            _js = js;
            _http = http;
        }
        private AuthenticationState Empty()
        {
            // not logged in
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>())));
        }
        private async Task Clean()
        {
            // bearer {token}
            await _js.RemoveItem(_tokenKey);
            _http.DefaultRequestHeaders.Authorization = null;
        }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await _js.GetItem(_tokenKey);

            if (string.IsNullOrWhiteSpace(token))
            {
                await Clean();
                return Empty();
            }


            _claims = ParseClaimsFromJwt(token);
            var expired = _claims.FirstOrDefault(p => p.Type == "exp")?.Value;

            if (IsExpired(expired))
            {
                await Clean();
                return Empty();
            }

            return SetAuth(token);
        }

        public AuthenticationState SetAuth(string token)
        {
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(ParseClaimsFromJwt(token) , "jwt")));
        }
        private bool IsExpired(string? expired)
        {
            if (string.IsNullOrEmpty(expired))
                return true;

            long expiredTotal = Convert.ToInt64(expired);

            DateTime currentDate = DateTime.UtcNow;
            long currentTotal = ((DateTimeOffset)currentDate).ToUnixTimeSeconds();

            if (expiredTotal > currentTotal)
                // not expired
                return false;
            else
                return true;
        }
        private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var claims = new List<Claim>();
            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
            return keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()));
        }
        private byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }

        public async Task Login(string token)
        {
            await _js.SetItem(_tokenKey , token);
            AuthenticationState state =  SetAuth(token);
            NotifyAuthenticationStateChanged(Task.FromResult(state));
        }

        public async Task Logout()
        {
            await Clean();
            NotifyAuthenticationStateChanged(Task.FromResult(Empty()));
        }
    }
}
