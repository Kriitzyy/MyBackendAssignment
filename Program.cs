using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Data;
using Repository;
using Services;

    /// <summary>
    /// The Entry point for the application
    /// </summary>
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configure database connection context with PostgreSQL
            builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Add controllers
            builder.Services.AddControllers();
            
            // Register application services
            builder.Services.AddScoped<IFolderRepository, FolderRepository>();
            builder.Services.AddScoped<IFileRepository, FileRepository>();
            builder.Services.AddScoped<IFolderService, FolderService>();
            builder.Services.AddScoped<IFileService, FileService>();
            var app = builder.Build();

            // Middleware
            app.UseHttpsRedirection();

            // Map controller routes
            app.MapControllers();
            
            /// <summary>
            /// Starts the application and begins listening for HTTP requests
            /// </summary>
            /// <remarks>
            /// This is a blocking call that keeps the application running until shutdown.
            /// The application will respond to incoming requests based on the configured middleware pipeline.
            /// </remarks>
            app.Run();
        }
    }
