using System.Threading.Tasks;
using Models;
using DTOs;
using Repository;

namespace Services {

    /// <summary>
    /// Service for handling file operations including upload, download and deletion
    /// </summary>
    public interface IFileService
    {
        /// <summary>
        /// Uploads a new file to the system
        /// </summary>
        Task<FileDto> UploadFileAsync(FileUploadDto dto);

        /// <summary>
        /// Downloads a file by its ID including the content
        /// </summary>
        Task<FileDownloadDto?> DownloadFileAsync(int id);

        /// <summary>
        /// Permanently deletes a file
        /// </summary>
        Task DeleteFileAsync(int id);
             
        /// <summary>
        /// Retrieves data for all files in the system
        /// </summary>
        Task<List<FileDto>> GetAllFilesAsync(); 

    }

    public class FileService : IFileService
    {

        private readonly IFileRepository _fileRepository;
        
        /// <summary>
        /// Initializes a new instance of the FileService
        /// </summary>
        public FileService(IFileRepository fileRepository)
        {
            _fileRepository = fileRepository;
        }

        /// <inheritdoc/>
        public async Task<FileDto> UploadFileAsync(FileUploadDto dto)
        {
            // In production, we would add size validation here
            var file = new Models.File
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

        /// <inheritdoc/>
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

        /// <inheritdoc/>
        public async Task DeleteFileAsync(int id)
        {
            // TODO: Add authorization checks in future implementation
            await _fileRepository.DeleteFileAsync(id);
        }
        
        /// <inheritdoc/>
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