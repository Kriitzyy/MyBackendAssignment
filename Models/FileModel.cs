using System.ComponentModel.DataAnnotations;
using Models;

namespace Models
{
    /// <summary>
    /// Represents a file entity in the system
    /// </summary>
    public class File
    {
        /// <summary>
        /// Primary key identifier
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Name of the file including extension (required)
        /// </summary>
        [Required]
        public string FileName { get; set; }
        
        /// <summary>
        /// Content of the file stored as text (required)
        /// </summary>
        [Required]
        public string Content { get; set; }

        /// <summary>
        /// Foreign key to containing folder
        /// </summary>
        public int FolderId { get; set; }

        /// <summary>
        /// Navigation property to containing folder
        /// </summary>
        public Folder Folder { get; set; } = null!;

    }

}   
