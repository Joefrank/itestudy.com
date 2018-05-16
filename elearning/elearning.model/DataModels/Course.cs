using elearning.model.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace elearning.model.DataModels
{
    [Table("Course")]
    public class Course
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MinLength(5)]
        public string Title { get; set; }
        public Guid? MainImageLink { get; set; }
        [Required]
        [MinLength(25)]
        public string Description { get; set; }
        [Required]
        public string ShortDescription { get; set; }
        public string YoutubeLink { get; set; }
        public int Status { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? LastModified { get; set; }
        public int? LastModifiedBy { get; set; }
        public int CategoryId { get; set; }
        public int CreatedBy { get; set; }
        public int ChapterCount { get; set; }
        public int LessonCount { get; set; }

        //navigation property
        public virtual CourseCategory Category { get; set; }
        public virtual User Creator { get; set; }
        public virtual User LastModifier{get;set;}
        public virtual ICollection<CourseChapter> Chapters { get; set; }
    }
}
