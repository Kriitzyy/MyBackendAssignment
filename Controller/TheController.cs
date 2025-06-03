using Microsoft.AspNetCore.Mvc;
using MyFolderDTO;
using FileDTO;
using FolderServices;
using FileServices;

namespace FileFolderAPI.Controllers {

    [Route("api/[controller]")]
    [ApiController]
    public class FileFolderController : ControllerBase
    {
        private readonly IFolderService _folderService;
        private readonly IFileService _fileService;

        public FileFolderController(IFolderService folderService, IFileService fileService)
        {

            _folderService = folderService;
            _fileService = fileService;
        }

        // Folder Endpoints

        [HttpPost("create-folders")]
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

        [HttpGet("GetFoldersById")]
        public async Task<ActionResult<FolderDTO>> GetFolder(int id)
        {

            var folder = await _folderService.GetFolderByIdAsync(id);
            if (folder == null)
            {

                return NotFound();
            }
            return Ok(folder);
        }

        // File Endpoints

        [HttpPost("UploadAFile")]
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

        [HttpGet("DownloadAFile")]
        public async Task<ActionResult<FileDownloadDto>> DownloadFile(int id)
        {

            var file = await _fileService.DownloadFileAsync(id);
            if (file == null)
            {

                return NotFound();
            }
            return Ok(file);
        }

        [HttpDelete("DeleteAFile")]
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

        // In FileFolderController.cs
        [HttpGet("GetAllFolders")]
        public async Task<ActionResult<List<FolderDTO>>> GetAllFolders()
        {
            var folders = await _folderService.GetAllFoldersAsync();
            return Ok(folders);
        }
 
        [HttpGet("GetAllFiles")]
        public async Task<ActionResult<List<FileDto>>> GetAllFiles()
        {
            var files = await _fileService.GetAllFilesAsync();
            return Ok(files);
        }
    }
}