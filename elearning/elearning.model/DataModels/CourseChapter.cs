﻿using System;
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
        public string Title { get; set; }
        [Required]
        [MinLength(25)]
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateLastModified { get; set; }
        public int StatusId { get; set; }
        public int CreatedBy { get; set; }
        public int LastModifiedBy { get; set; }
        public int LessonCount { get; set; }
        public int TutorialCount { get; set; }

        public virtual ICollection<Lesson> lessons { get; set; }
    }
}
