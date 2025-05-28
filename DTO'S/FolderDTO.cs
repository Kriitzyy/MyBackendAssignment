using System.ComponentModel.DataAnnotations;
using FileDTO;

namespace FolderDTOS {
    
    // 1. Skapa mapp
    public class FolderCreateDTO {
        

        [Required]
        public string Name { get; set; }  = string.Empty; 

        public int? ParentFolderId { get; set; }
    }

    public class FolderDTO {

        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }

        public int? ParentFolderId { get; set; }
        
        public List<FolderDTO>? SubFolders { get; set; }

        public List<FileDto>? Files { get; set; }
    }
   
}