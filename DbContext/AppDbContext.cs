using Microsoft.EntityFrameworkCore;
using FolderModel;
using FileModel;
using FolderServices;

namespace MyDbContext {
    
    public class AppDbContext : DbContext {

        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

        public DbSet<Folder> Folders { get; set; }
        public DbSet<FileModel.File> Files { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder) {

            modelBuilder.Entity<Folder>()
                .HasMany(f => f.SubFolders)
                .WithOne(f => f.ParentFolder)
                .HasForeignKey(f => f.ParentFolderId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Folder>()
                .HasMany(f => f.Files)
                .WithOne(f => f.Folder)
                .HasForeignKey(f => f.FolderId)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
