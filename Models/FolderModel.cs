using System.ComponentModel.DataAnnotations;

namespace Models
{
    /// <summary>
    /// Represents a folder entity in the hierarchical files system
    /// </summary>
    public class Folder
    {

        /// <summary>
        /// Primary key identifier
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Name of the folder (required)
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Reference to parent folder (nullable for root folders)
        /// </summary>
        public int? ParentFolderId { get; set; }

        /// <summary>
        /// Navigation property to parent folder
        /// </summary>
        public Folder? ParentFolder { get; set; }
        
        /// <summary>
        /// Collection of child folders
        /// </summary>
        public List<Folder>? SubFolders { get; set; } = new List<Folder>();

        
        /// <summary>
        /// Collection of files contained in this folder
        /// </summary>
        public List<Models.File>? Files { get; set; } = new List<Models.File>();


    }
}