using Microsoft.EntityFrameworkCore;
using Models; 
using Services;

namespace Data
{
    /// <summary>
    /// Database context for the file folder application
    /// </summary>
    public class AppDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the database context
        /// </summary>
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

        /// <summary>
        /// DbSet for folder entities
        /// </summary>
        public DbSet<Folder> Folders { get; set; }

        /// <summary>
        /// DbSet for file entities
        /// </summary>
        public DbSet<Models.File> Files { get; set; }

        /// <summary>
        /// Configures the model relationships and constraints
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure folder hierarchy relationships
            modelBuilder.Entity<Folder>()
                .HasMany(f => f.SubFolders)
                .WithOne(f => f.ParentFolder)
                .HasForeignKey(f => f.ParentFolderId)
                .OnDelete(DeleteBehavior.Restrict);
                
            // Configure folder-file relationship
            modelBuilder.Entity<Folder>()
                .HasMany(f => f.Files)
                .WithOne(f => f.Folder)
                .HasForeignKey(f => f.FolderId)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
