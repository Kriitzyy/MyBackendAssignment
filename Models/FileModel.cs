using System.ComponentModel.DataAnnotations;
using Models;

namespace Models
{

    public class File
    {

        [Key]
        public int Id { get; set; }
        [Required]
        public string FileName { get; set; }
        [Required]
        public string Content { get; set; }

        // Foreignkey for the FolderID 
        public int FolderId { get; set; }
        // Folder is the navigation to the folderobject
        public Folder Folder { get; set; } = null!;

    }

}   
