using System.ComponentModel.DataAnnotations;

namespace DTOs {
    
    /// <summary>
    /// Data transfer object representing basic file data
    /// </summary>
    public class FileDto
    {
        /// <summary>
        /// File ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// File name with extension
        /// </summary>
        public string FileName { get; set; } = string.Empty;

        /// <summary>
        /// ID of containing folder
        /// </summary>
        public int FolderId { get; set; }

    }

    /// <summary>
    /// Data transfer object for uploading files
    /// </summary>
    public class FileUploadDto {
        
        /// <summary>
        /// Name of the file including extension (required)
        /// </summary>
        [Required]    
        public string FileName { get; set; } 

        /// <summary>
        /// File content as text (required)
        /// </summary>
        [Required]                                                                      
        [DataType(DataType.Text)]
        public string Content { get; set; } = string.Empty; 

        /// <summary>
        /// ID of target folder (required)
        /// </summary>
        [Required]
        public int FolderId { get; set; }
}


    public class FileDeleteDto {

        [Required]
        public int Id { get; set; }
}


    /// <summary>
    /// Data transfer object for downloading files including content
    /// </summary>
    public class FileDownloadDto
    {
        /// <summary>
        /// File ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// File name with extension
        /// </summary>
        public string FileName { get; set; } = string.Empty;
        
        /// <summary>
        /// File content
        /// </summary>
        public string Content { get; set; }
    }


}
