
using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace elearning.model.ViewModels
{
   public class EditArticleVm
    {
        public int ArticleId { get; set; }

        [Required(ErrorMessage ="Title is required")]
        public string Title { get; set; }
        
        [AllowHtml]
        [Required(ErrorMessage = "Content is required")]

        public string Content { get; set; }
        public Guid? MainImageLink { get; set; }
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
