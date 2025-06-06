using System.ComponentModel.DataAnnotations;
using DTOs;

namespace DTOs {
    
    /// <summary>
    /// Data transfer object for creating a new folder
    /// </summary>
    public class FolderCreateDTO
    {

        /// <summary>
        /// Name of the new folder (required)
        /// </summary>
        [Required]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Optional ID of parent folder (null for root folder)
        /// </summary>
        public int? ParentFolderId { get; set; }
    }

    /// <summary>
    /// Data transfer object representing a folder with its contents
    /// </summary>
    public class FolderDTO {

        /// <summary>
        /// Folder ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Folder name
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// ID of parent folder (null for root folder)
        /// </summary>
        public int? ParentFolderId { get; set; }
        
        /// <summary>
        /// List of child folders (null if not expanded)
        /// </summary>
        public List<FolderDTO>? SubFolders { get; set; }
        
        /// <summary>
        /// List of files in this folder (null if not expanded)
        /// </summary>
        public List<FileDto>? Files { get; set; }
    }
   
}