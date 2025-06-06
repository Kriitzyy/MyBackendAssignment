using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models; 
using Data;


namespace Repository
{

    public interface IFolderRepository
    {

        Task<Folder> CreateFolderAsync(Folder folder);
        Task<Folder?> GetFolderByIdAsync(int id);
        Task<List<Folder>> GetAllFoldersAsync();
    }

    public class FolderRepository : IFolderRepository
    {

        private readonly AppDbContext _context;

        public FolderRepository(AppDbContext context)
        {

            _context = context;
        }

        public async Task<Folder> CreateFolderAsync(Folder folder)
        {

            _context.Folders.Add(folder);
            await _context.SaveChangesAsync();
            return folder;
        }

        public async Task<Folder?> GetFolderByIdAsync(int id)
        {

            return await _context.Folders
                .Include(f => f.SubFolders)
                .Include(f => f.Files)
                .FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<List<Folder>> GetAllFoldersAsync()
        {
            return await _context.Folders
            .Include(f => f.SubFolders)
            .Include(f => f.Files)
            .ToListAsync();
        }
    }
}