using System.Threading.Tasks;
using FileDTO;
using FolderModel;
using FolderDTOS;
using FolderRepo;

namespace FolderServices {

    public interface IFolderService
    {

        Task<FolderDTO> CreateFolderAsync(FolderCreateDTO dto);
        Task<FolderDTO?> GetFolderByIdAsync(int id);
        Task<List<FolderDTO>> GetAllFoldersAsync();

    }

    public class FolderService : IFolderService
    {

        private readonly IFolderRepository _folderRepository;

        public FolderService(IFolderRepository folderRepository)
        {

            _folderRepository = folderRepository;
        }

        public async Task<FolderDTO> CreateFolderAsync(FolderCreateDTO dto)
        {

            var folder = new Folder
            {

                Name = dto.Name,
                ParentFolderId = dto.ParentFolderId,
                SubFolders = new List<Folder>(),    // Initialize collections in model
                Files = new List<FileModel.File>()  // 

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