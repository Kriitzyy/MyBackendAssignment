using Microsoft.EntityFrameworkCore;
using FolderModel;
using FileModel;

namespace MyDbContext
{
    public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<Folder> Folders { get; set; }
    public DbSet<FileModel.File> Files { get; set; }

   
}
}
