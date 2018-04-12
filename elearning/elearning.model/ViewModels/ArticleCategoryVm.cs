using System;
using elearning.model.Enums;
using System.ComponentModel.DataAnnotations;
using elearning.model.DataModels;

namespace elearning.model.ViewModels
{
   public class ArticleCategoryVm
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="You need to provide category name")]
        public string Name { get; set; }
        public string Description { get; set; }
        public int CreatedBy { get; set; }
        public int? LastModifiedBy { get; set; }
        public ArticleCategoryStatus Status { get; set; }

        public bool ShowError { get; set; }
    }

    public class ArticleCategoryDetailsVm : ArticleCategoryVm
    {
        public User Creator { get; set; }
        public DateTime DateCreated { get; set; }
        public User LastModifier { get; set; }
        public DateTime? LastModified { get; set; }
    }
}
