using Microsoft.EntityFrameworkCore;
using Data;

namespace Repository {

    /// <summary>
    /// Interface defining operations for file data access
    /// </summary>
    public interface IFileRepository
    {
        /// <summary>
        /// Uploads a file to the repository
        /// </summary>
        /// <param name="file">The file entity to upload</param>
        /// <returns>The uploaded file entity</returns>
        Task<Models.File> UploadFileAsync(Models.File file);
        
        /// <summary>
        /// Retrieves a file by its unique identifier
        /// </summary>
        /// <param name="id">The ID of the file to retrieve</param>
        /// <returns>The file entity if found, otherwise null</returns>
        Task<Models.File?> GetFileByIdAsync(int id);

        /// <summary>
        /// Deletes a file from the repository
        /// </summary>
        /// <param name="id">The ID of the file to delete</param>
        Task DeleteFileAsync(int id);
        
        /// <summary>
        /// Retrieves all files from the repository
        /// </summary>
        /// <returns>A list of all file entities</returns>
        Task<List<Models.File>> GetAllFilesAsync(); // Add this method

    }

    /// <summary>
    /// Repository implementation for file data operations
    /// </summary>
    public class FileRepository : IFileRepository
    {
        private readonly AppDbContext _context;
        
        /// <summary>
        /// Initializes a new instance of the FileRepository
        /// </summary>
        /// <param name="context">The database context to use</param>
        public FileRepository(AppDbContext context)
        {
            _context = context;
        }

        /// <inheritdoc/>
        public async Task<Models.File> UploadFileAsync(Models.File file)
        {
            _context.Files.Add(file);
            await _context.SaveChangesAsync();
            return file;
        }

        /// <inheritdoc/>
        public async Task<Models.File?> GetFileByIdAsync(int id)
        {
            return await _context.Files.FindAsync(id);
        }

        /// <inheritdoc/>
        public async Task DeleteFileAsync(int id)
        {
            var file = await _context.Files.FindAsync(id);

            if (file != null)
            {
                _context.Files.Remove(file);
                await _context.SaveChangesAsync();
            }
        }
        
        /// <inheritdoc/>
        public async Task<List<Models.File>> GetAllFilesAsync()
        {
            return await _context.Files.ToListAsync();
        }
    }
}