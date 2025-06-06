using Microsoft.AspNetCore.Mvc;
using DTOs;
using Services;


namespace FileFolderAPI.Controllers
{
    /// <summary>
    /// API controller to manage files and folders operations in a hierarchy structure
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class FileFolderController : ControllerBase
    {
        private readonly IFolderService _folderService;
        private readonly IFileService _fileService;

        /// <summary>
        /// Initialize a new instance of my FileFolderController
        /// </summary>
        /// <param name="folderService">Folder service</param>
        /// <param name="fileService">File service</param>
        public FileFolderController(IFolderService folderService, IFileService fileService)
        {

            _folderService = folderService;
            _fileService = fileService;
        }

        /// <summary>
        /// Creates a new folder in the file system hierarchy
        /// </summary>
        /// <param name="dto">Folder creation data</param>
        /// <returns>The newly created folder</returns>
        /// <response code="201">Folder created successfully</response>
        /// <response code="400">Invalid request data</response>
        [HttpPost("folders")]
        public async Task<ActionResult<FolderDTO>> CreateFolder(FolderCreateDTO dto)
        {

            try
            {
                var folder = await _folderService.CreateFolderAsync(dto);
                return CreatedAtAction(nameof(GetFolder), new { id = folder.Id }, folder);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Retrieves a specific folder by its ID including all subfolders and files
        /// </summary>
        /// <param name="id">The ID of the folder to retrieve</param>
        /// <returns>The requested folder structure</returns>
        /// <response code="200">Folder found and returned</response>
        /// <response code="404">Folder not found</response>
        [HttpGet("folders/{id}")]
        public async Task<ActionResult<FolderDTO>> GetFolder(int id)
        {

            var folder = await _folderService.GetFolderByIdAsync(id);
            if (folder == null)
            {

                return NotFound();
            }
            return Ok(folder);
        }

        /// <summary>
        /// Uploads a new file to a specified folder by id
        /// </summary>
        /// <param name="dto">File upload data including content</param>
        /// <returns>The created file metadata</returns>
        /// <response code="201">File uploaded successfully</response>
        /// <response code="400">Invalid request data</response>
        [HttpPost("files")]
        public async Task<ActionResult<FileDto>> UploadFile([FromBody] FileUploadDto dto)
        {

            try
            {

                var file = await _fileService.UploadFileAsync(dto);
                return CreatedAtAction(nameof(DownloadFile), new { id = file.Id }, file);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Downloads a file by its ID including the content
        /// </summary>
        /// <param name="id">The ID of the file to download</param>
        /// <returns>The file content and metadata</returns>
        /// <response code="200">File found and returned</response>
        /// <response code="404">File not found</response>
        [HttpGet("files/{id}")]
        public async Task<ActionResult<FileDownloadDto>> DownloadFile(int id)
        {

            var file = await _fileService.DownloadFileAsync(id);
            if (file == null)
            {

                return NotFound();
            }
            return Ok(file);
        }

        /// <summary>
        /// Deletes a file by its ID
        /// </summary>
        /// <param name="id">The ID of the file to delete</param>
        /// <returns>No content if it's successful</returns>
        /// <response code="204">File deleted successfully</response>
        /// <response code="400">Invalid request</response>
        [HttpDelete("filess/{id}")]
        public async Task<IActionResult> DeleteFile(int id)
        {
            try
            {
                await _fileService.DeleteFileAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Retrieves all folders in the system with their hierarchy
        /// </summary>
        /// <returns>List of all folders</returns>
        [HttpGet("folders")]
        public async Task<ActionResult<List<FolderDTO>>> GetAllFolders()
        {
            var folders = await _folderService.GetAllFoldersAsync();
            return Ok(folders);
        }

        /// <summary>
        /// Retrieves all files in the system
        /// </summary>
        /// <returns>List of all files</returns>
        [HttpGet("files")]
        public async Task<ActionResult<List<FileDto>>> GetAllFiles()
        {
            var files = await _fileService.GetAllFilesAsync();
            return Ok(files);
        }
    }
}