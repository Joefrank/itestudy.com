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
        public int LessonID { get; set; }
        [Required]
        [MinLength(5)]
        public string title { get; set; }
        [Required]
        [MinLength(25)]
        public string resume { get; set; }
        [Required]
        [MinLength(25)]
        public string content { get; set; }
        public DateTime dateCreated { get; set; }
        public DateTime dateLastModified { get; set; }
        public Boolean status { get; set; }
        public int createdBy { get; set; }
        public int lastModifiedBy { get; set; }
        public int relatedChapterID { get; set; }
        public virtual ICollection<Lesson> lessons { get; set; }
    }
}
