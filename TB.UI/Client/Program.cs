using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using TB.UI;
using TB.UI.Services;
using TB.UI.Services.AuthService;
using TB.UI.Services.Repository;
using TB.UI.Services.Site;
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
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<ISiteService, SiteService>();
            builder.Services.AddScoped<JWTService>();
            builder.Services.AddScoped<AuthenticationStateProvider, JWTService>(op =>
                  op.GetRequiredService<JWTService>()
            );
            builder.Services.AddScoped<IJWTAuthService, JWTService>(op =>
                  op.GetRequiredService<JWTService>()
            );
            builder.Services.AddMudServices();
            builder.Services.AddAuthorizationCore();
            builder.Services.AddFileReaderService();
            await builder.Build().RunAsync();
        }
    }
}