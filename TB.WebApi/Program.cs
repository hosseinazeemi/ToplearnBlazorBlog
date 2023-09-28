using Microsoft.EntityFrameworkCore;
using TB.Application.Interfaces;
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

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}