using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using TB.UI;
using TB.UI.Services;
using Tewr.Blazor.FileReader;

namespace TB.UI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddScoped<IHttpClientService, HttpClientService>();
            builder.Services.AddMudServices();
            builder.Services.AddFileReaderService();
            await builder.Build().RunAsync();
        }
    }
}