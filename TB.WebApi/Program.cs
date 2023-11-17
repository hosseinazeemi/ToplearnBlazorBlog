using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;
using TB.Application.Interfaces;
using TB.Application.Services;
using TB.Persistence.Context;
using TB.WebApi.Services;

namespace TB.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            string connection = builder.Configuration.GetSection("ConnectionStrings")["DefaultConnection"];
            builder.Services.AddDbContext<AppDbContext>(p => p.UseSqlServer(connection));
            builder.Services.AddScoped<IAppDbContext , AppDbContext>();
            builder.Services.AddScoped<IFileService , FileService>();
            builder.Services.AddScoped<ICategoryService , CategoryService>();
            builder.Services.AddScoped<IUserService , UserService>();
            builder.Services.AddScoped<IContentService , ContentService>();
            builder.Services.AddScoped<ICommentService , CommentService>();
            builder.Services.AddScoped<ISiteService , SiteService>();
            builder.Services.AddScoped<ISettingService , SettingService>();

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(op =>
                {
                    op.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true , 
                        ValidateIssuerSigningKey = true ,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(builder.Configuration.GetValue<string>("JWTKey")))
                    };
                });
            string clientUrl = builder.Configuration.GetValue<string>("ClientUrl");
            builder.Services.AddCors(option =>
            {
                option.AddDefaultPolicy(p =>
                {
                    p.WithOrigins(clientUrl)
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            builder.Services.AddHttpContextAccessor();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseCors();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}