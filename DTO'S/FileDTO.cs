using System.ComponentModel.DataAnnotations;

namespace FileDTO {

    public class FileDto {

    public int Id { get; set; }
    public string FileName { get; set; } = string.Empty;
    public int FolderId { get; set; }
    
    }


    // 2. Ladda upp fil
    public class FileUploadDto {
    
    [Required]
    public string FileName { get; set; } 

    [Required]
    [DataType(DataType.Text)]
    public string Content { get; set; } = string.Empty; 
    [Required]
    public int FolderId { get; set; }
}

    // 3. Radera fil
    public class FileDeleteDto {

    [Required]
    public int Id { get; set; }
}

    // 4. Ladda ned fil (response DTO)
    public class FileDownloadDto {

    public int Id { get; set; }
    public string FileName { get; set; } = string.Empty;
    public string Content { get; set; } 
}


}
