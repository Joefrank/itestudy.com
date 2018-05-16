using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace elearning.model.DataModels
{
    [Table("Lesson")]
    public class Lesson
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ChapterId { get; set; }
        [Required]
        [MinLength(5)]
        public string Title { get; set; }
        [Required]
        [MinLength(25)]
        public string Content { get; set; }
        [Required]
        [MinLength(25)]
        public string Overview { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateLastModified { get; set; }
        public int Status { get; set; }
        public int CreatedBy { get; set; }
        public int LastModifiedBy { get; set; }
        public int TutorialCount { get; set; }

        public virtual ICollection<Lesson> lessons { get; set; }
    }
}
