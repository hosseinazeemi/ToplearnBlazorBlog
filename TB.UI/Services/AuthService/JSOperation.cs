using Microsoft.JSInterop;

namespace TB.UI.Services.AuthService
{
    public static class JSOperation
    {
        public static async Task<string> GetItem(this IJSRuntime js, string key)
        {
            // localStorage.getItem(key)
            return await js.InvokeAsync<string>("localStorage.getItem" , key);
        }

        public static async Task SetItem(this IJSRuntime js , string key , string value)
        {
            // localStorage.setItem(key , value)
            await js.InvokeVoidAsync("localStorage.setItem" , key , value);
        }

        public static async Task RemoveItem(this IJSRuntime js , string key)
        {
            await js.InvokeVoidAsync("localStorage.removeItem" , key);
        }
    }
}
