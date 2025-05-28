using System.Threading.Tasks;
using FileDTO;
using FileModel;
using FileRepo;
using FolderDTOS;

namespace FileServices {

    public interface IFileService
    {

        Task<FileDto> UploadFileAsync(FileUploadDto dto);
        Task<FileDownloadDto?> DownloadFileAsync(int id);
        Task DeleteFileAsync(int id);
        Task<List<FileDto>> GetAllFilesAsync(); // Add this method

    }

    public class FileService : IFileService
    {

        private readonly IFileRepository _fileRepository;

        public FileService(IFileRepository fileRepository)
        {
            _fileRepository = fileRepository;
        }

        public async Task<FileDto> UploadFileAsync(FileUploadDto dto)
        {

            var file = new FileModel.File
            {

                FileName = dto.FileName,
                Content = dto.Content,
                FolderId = dto.FolderId
            };

            var createdFile = await _fileRepository.UploadFileAsync(file);

            return new FileDto
            {

                Id = createdFile.Id,
                FileName = createdFile.FileName,
                FolderId = createdFile.FolderId
            };
        }

        public async Task<FileDownloadDto?> DownloadFileAsync(int id)
        {

            var file = await _fileRepository.GetFileByIdAsync(id);
            if (file == null) return null;

            return new FileDownloadDto
            {

                Id = file.Id,
                FileName = file.FileName,
                Content = file.Content
            };
        }

        public async Task DeleteFileAsync(int id)
        {

            await _fileRepository.DeleteFileAsync(id);
        }
        
        public async Task<List<FileDto>> GetAllFilesAsync()
        {
            var files = await _fileRepository.GetAllFilesAsync();
            return files.ConvertAll(f => new FileDto
            {
                Id = f.Id,
                FileName = f.FileName,
                FolderId = f.FolderId
            });
        }
        
    }
}