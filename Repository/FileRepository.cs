using Microsoft.EntityFrameworkCore;
using MyDbContext;

namespace FileRepo {

    public interface IFileRepository {

        Task<FileModel.File> UploadFileAsync(FileModel.File file);
        Task<FileModel.File?> GetFileByIdAsync(int id);
        Task DeleteFileAsync(int id);
    }

    public class FileRepository : IFileRepository
    {
        private readonly AppDbContext _context;

        public FileRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<FileModel.File> UploadFileAsync(FileModel.File file)
        {
            _context.Files.Add(file);
            await _context.SaveChangesAsync();
            return file;
        }

        public async Task<FileModel.File?> GetFileByIdAsync(int id)
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
    }
}