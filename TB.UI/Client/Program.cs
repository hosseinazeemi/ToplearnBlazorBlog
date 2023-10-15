using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using TB.UI;
using TB.UI.Services;
using TB.UI.Services.Repository;
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

            string apiBaseUrl = builder.Configuration.GetValue<string>("ApiBaseUrl");
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(apiBaseUrl) });
            builder.Services.AddScoped<IHttpClientService, HttpClientService>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IContentService, ContentService>();
            builder.Services.AddScoped<ICommentService, CommentService>();
            builder.Services.AddMudServices();
            builder.Services.AddFileReaderService();
            await builder.Build().RunAsync();
        }
    }
}