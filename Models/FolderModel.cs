using System.ComponentModel.DataAnnotations;

namespace FolderModel {
    
    public class Folder {

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int? ParentFolderId { get; set; }

        public Folder? ParentFolder { get; set; }

        public List<Folder>? SubFolders { get; set; } =  new List<Folder>();
        
        public List<FileModel.File>? Files { get; set; } = new List<FileModel.File>();
        

    }
}