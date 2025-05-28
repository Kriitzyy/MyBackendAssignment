using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MyDbContext;
using FolderRepo;
using FileRepo;
using FolderRepo;
using FolderServices;
using FileServices;

namespace TheMain
{

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add DbContext with PostgreSQL
            builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Add controllers
            builder.Services.AddControllers();

            builder.Services.AddScoped<IFolderRepository, FolderRepository>();
            builder.Services.AddScoped<IFileRepository, FileRepository>();
            builder.Services.AddScoped<IFolderService, FolderService>();
            builder.Services.AddScoped<IFileService, FileService>();
            var app = builder.Build();

            // Middleware
            app.UseHttpsRedirection();

            // Map controller routes
            app.MapControllers();

            app.Run();
        }
    }
}