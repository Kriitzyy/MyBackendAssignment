using System.Threading.Tasks;
using Models;
using Repository;
using DTOs;

namespace Services {

    /// <summary>
    /// Service interface for folder operations
    /// </summary>
    public interface IFolderService
    {
        /// <summary>
        /// Creates a new folder from DTO
        /// </summary>
        Task<FolderDTO> CreateFolderAsync(FolderCreateDTO dto);

        /// <summary>
        /// Gets a folder by ID with its structure
        /// </summary>
        Task<FolderDTO?> GetFolderByIdAsync(int id);

        /// <summary>
        /// Gets all folders with their hierarchy
        /// </summary>
        Task<List<FolderDTO>> GetAllFoldersAsync();

    }

    /// <inheritdoc/>
    public class FolderService : IFolderService
    {

        private readonly IFolderRepository _folderRepository;

        /// <summary>
        /// Initializes a new instance of the folder service
        /// </summary>
        public FolderService(IFolderRepository folderRepository)
        {

            _folderRepository = folderRepository;
        }
        
        /// <summary>
        /// Creates a new folder in the system
        /// </summary>
        /// <param name="dto">Data transfer object containing folder creation details</param>
        /// <returns>A <see cref="FolderDTO"/> representing the created folder</returns>
        public async Task<FolderDTO> CreateFolderAsync(FolderCreateDTO dto)
        {

            var folder = new Folder
            {

                Name = dto.Name,
                ParentFolderId = dto.ParentFolderId,
                SubFolders = new List<Folder>(),
                Files = new List<Models.File>()

            };

            var createdFolder = await _folderRepository.CreateFolderAsync(folder);

            return new FolderDTO
            {

                Id = createdFolder.Id, // Fixed property name
                Name = createdFolder.Name,
                ParentFolderId = createdFolder.ParentFolderId,
                SubFolders = new List<FolderDTO>(),  // Initialize as empty list in DTO
                Files = new List<FileDto>()
            };
        }

        /// <summary>
        /// Retrieves a folder by its unique identifier
        /// </summary>
        /// <param name="id">The ID of the folder to retrieve</param>
        /// <returns>A <see cref="FolderDTO"/> representing the folder, or null if not found</returns>
        public async Task<FolderDTO?> GetFolderByIdAsync(int id)
        {

            var folder = await _folderRepository.GetFolderByIdAsync(id);
            if (folder == null) return null;

            return new FolderDTO
            {

                Id = folder.Id, // Fixed property name
                Name = folder.Name,
                ParentFolderId = folder.ParentFolderId,
                SubFolders = folder.SubFolders?.ConvertAll(f => new FolderDTO
                {
                    Id = f.Id, // Fixed property name
                    Name = f.Name,
                    ParentFolderId = f.ParentFolderId
                }),
                Files = folder.Files?.ConvertAll(f => new FileDto
                {
                    Id = f.Id,
                    FileName = f.FileName,
                    FolderId = f.FolderId
                })
            };

        }

        /// <summary>
        /// Retrieves all folders in the system with their hierarchical structure
        /// </summary>
        /// <returns>A list of <see cref="FolderDTO"/> objects representing all folders</returns>
        public async Task<List<FolderDTO>> GetAllFoldersAsync()
        {
            var folders = await _folderRepository.GetAllFoldersAsync();
            return folders.ConvertAll(f => new FolderDTO
            {
                Id = f.Id,
                Name = f.Name,
                ParentFolderId = f.ParentFolderId,
                SubFolders = f.SubFolders?.ConvertAll(sf => new FolderDTO
                {
                    Id = sf.Id,
                    Name = sf.Name,
                    ParentFolderId = sf.ParentFolderId
                }),
                Files = f.Files?.ConvertAll(file => new FileDto
                {
                    Id = file.Id,
                    FileName = file.FileName,
                    FolderId = file.FolderId
                })
            });
        }
    }
}