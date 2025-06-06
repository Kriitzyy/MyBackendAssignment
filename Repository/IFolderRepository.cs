using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models; 
using Data;

namespace Repository {

    /// <summary>
    /// Repository interface for folder operations
    /// </summary>
    public interface IFolderRepository
    {
        /// <summary>
        /// Creates a new folder in the database
        /// </summary>
        Task<Folder> CreateFolderAsync(Folder folder);
        
        /// <summary>
        /// Retrieves a folder by ID including its subfolders and files
        /// </summary>
        Task<Folder?> GetFolderByIdAsync(int id);

        /// <summary>
        /// Retrieves all folders with their hierarchy
        /// </summary>
        Task<List<Folder>> GetAllFoldersAsync();
    }

    /// <inheritdoc/>
    public class FolderRepository : IFolderRepository
    {

        private readonly AppDbContext _context;

        /// <summary>
        /// Initializes a new instance of the folder repository
        /// </summary>
        public FolderRepository(AppDbContext context)
        {

            _context = context;
        }

        /// <inheritdoc/>
        public async Task<Folder> CreateFolderAsync(Folder folder)
        {

            _context.Folders.Add(folder);
            await _context.SaveChangesAsync();
            return folder;
        }

        /// <inheritdoc/>
        public async Task<Folder?> GetFolderByIdAsync(int id)
        {

            return await _context.Folders
                .Include(f => f.SubFolders)
                .Include(f => f.Files)
                .FirstOrDefaultAsync(f => f.Id == id);
        }

        /// <inheritdoc/>
        public async Task<List<Folder>> GetAllFoldersAsync()
        {
            return await _context.Folders
            .Include(f => f.SubFolders)
            .Include(f => f.Files)
            .ToListAsync();
        }
    }
}