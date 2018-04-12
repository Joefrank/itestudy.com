using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace elearning.model.DataModels
{
    [Table("CourseChapter")]
    public class CourseChapter
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int chapterID { get; set; }
        [Required]
        [MinLength(5)]
        public string title { get; set; }
        [Required]
        [MinLength(25)]
        public string description { get; set; }
        public DateTime dateCreated { get; set; }
        public DateTime dateLastModified { get; set; }
        public Boolean status { get; set; }
        public int createdBy { get; set; }
        public int lastModifiedBy { get; set; }
        public int relatedCourseID { get; set; }
        public virtual ICollection<Lesson> lessons { get; set; }
    }
}
