
using System;

namespace elearning.model.ViewModels
{
    public class CourseChapterVm
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateLastModified { get; set; }
        public int StatusId { get; set; }
        public string Status { get; set; }
        public int CreatedBy { get; set; }
        public int LastModifiedBy { get; set; }
        public int LessonCount { get; set; }
        public int TutorialCount { get; set; }
        public string Creator { get; set; }
        public string LastModifier { get; set; }
    }
}
