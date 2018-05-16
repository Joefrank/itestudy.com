using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace elearning.model.DataModels
{
    [Table("Tutorial")]
    public class Tutorial
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int LessonId { get; set; }
        [Required]
        [MinLength(5)]
        public string Title { get; set; }
        [Required]
        [MinLength(25)]
        public string Content { get; set; }
        [Required]
        [MinLength(25)]
        public string Overview { get; set; }
        public string VideoLink { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime DateLastModified { get; set; }
        public int Status { get; set; }
        public int CreatedBy { get; set; }
        public int LastModifiedBy { get; set; }
        
    }
}
