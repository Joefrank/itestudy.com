
using System;
using System.ComponentModel.DataAnnotations;


namespace elearning.model.ViewModels
{
   public class EditArticleVm
    {
        [Required(ErrorMessage ="Title is required")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Content is required")]
        public string Content { get; set; }
        public int? MainImageLink { get; set; }
        public string YoutubeLinks { get; set; }
        public DateTime DateCreated { get; set; }
        public int CreatedBy { get; set; }
        [Required(ErrorMessage = "Category is required")]
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "Type is required")]
        public int Type { get; set; }
        public int? RelatedObjectTypeId { get; set; }
        public int? RelatedObjectId { get; set; }
        public DateTime? LastModified { get; set; }
        public int? LastModifiedBy { get; set; }
    }
}
