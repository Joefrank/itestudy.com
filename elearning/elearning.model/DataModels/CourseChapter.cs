using elearning.model.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace elearning.model.DataModels
{
    [Table("CourseChapter")]
    public class CourseChapter
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int CourseId { get; set; }
        [Required]
        [MinLength(5)]
        [SearchableCG]
        [EditableCG(1)]
        public string Title { get; set; }
        [Required]
        [MinLength(25)]
        [SearchableCG]
        [EditableCG(1)]
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateLastModified { get; set; }
        [EditableCG(1)]
        public int StatusId { get; set; }
        public int CreatedBy { get; set; }
        [EditableCG(2)]
        public int? LastModifiedBy { get; set; }
        [EditableCG(2)]
        public int LessonCount { get; set; }
        [EditableCG(2)]
        public int TutorialCount { get; set; }

        public virtual User Creator { get; set; }
        public virtual User LastModifier { get; set; }

        public virtual ICollection<Lesson> lessons { get; set; }
    }
}
