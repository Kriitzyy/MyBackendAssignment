using Microsoft.EntityFrameworkCore;
using Data;

namespace Repository {

    public interface IFileRepository
    {

        Task<Models.File> UploadFileAsync(Models.File file);
        Task<Models.File?> GetFileByIdAsync(int id);
        Task DeleteFileAsync(int id);
        Task<List<Models.File>> GetAllFilesAsync(); // Add this method

    }

    public class FileRepository : IFileRepository
    {
        private readonly AppDbContext _context;

        public FileRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Models.File> UploadFileAsync(Models.File file)
        {
            _context.Files.Add(file);
            await _context.SaveChangesAsync();
            return file;
        }

        public async Task<Models.File?> GetFileByIdAsync(int id)
        {
            return await _context.Files.FindAsync(id);
        }

        public async Task DeleteFileAsync(int id)
        {
            var file = await _context.Files.FindAsync(id);

            if (file != null)
            {
                _context.Files.Remove(file);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<List<Models.File>> GetAllFilesAsync()
        {
            return await _context.Files.ToListAsync();
        }
    }
}