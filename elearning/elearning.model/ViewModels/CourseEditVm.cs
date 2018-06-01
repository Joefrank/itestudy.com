using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace elearning.model.ViewModels
{
    public class CourseEditVm
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Title is required.")]
        [MinLength(5, ErrorMessage = "Title is too short. (min 5 characters)")]
        public string Title { get; set; }
        public Guid? MainImageLink { get; set; }
        [Required]
        [MinLength(25, ErrorMessage = "Description is too short. (min 25 characters)")]
        public string Description { get; set; }
        [Required(ErrorMessage = "ShortDescription is required.")]
        public string ShortDescription { get; set; }
        public string YoutubeLink { get; set; }
        public int Status { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? LastModified { get; set; }
        public int? LastModifiedBy { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public int CreatedBy { get; set; }

        public List<GlossaryVm> CourseCategories { get; set; }
        public bool ShowError { get; set; }
    }
}
