
using elearning.model.DataModels;
using elearning.model.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace elearning.model.ViewModels
{
    public class CourseCategoryEditVm
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "You need to provide category name")]
        public string Name { get; set; }
        public string Description { get; set; }
        public int CreatedBy { get; set; }
        public int? LastModifiedBy { get; set; }
        public int Status { get; set; }

        public User Creator { get; set; }
        public DateTime DateCreated { get; set; }
        public User LastModifier { get; set; }
        public DateTime? LastModified { get; set; }

        public bool ShowError { get; set; }
    }
}
