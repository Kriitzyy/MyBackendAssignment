using System.ComponentModel.DataAnnotations;

namespace Models
{

    public class Folder
    {

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int? ParentFolderId { get; set; }

        public Folder? ParentFolder { get; set; }

        public List<Folder>? SubFolders { get; set; } = new List<Folder>();

        public List<Models.File>? Files { get; set; } = new List<Models.File>();


    }
}