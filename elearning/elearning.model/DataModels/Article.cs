using elearning.model.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace elearning.model.DataModels
{
    [Table("Article")]
    public class Article
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(1000)]
        public string Title { get; set; }
        [Required]
        [MinLength(25)]
        public string Content { get; set; }
        public Guid? MainImageLink { get; set; }

        [MaxLength(200)]
        public string YoutubeLinks { get; set; }
        public int CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? LastModified { get; set; }
        public int? LastModifiedBy { get; set; }
        public int Status { get; set; }
        public int Type { get; set; }
        public int CategoryId { get; set; }
        public int? RelatedObjectTypeId { get; set; }
        public int? RelatedObjectId { get; set; }

        //navigation property
        public virtual ArticleCategory Categorie { get; set; }
        public virtual User Creator { get; set; }
        public virtual User Modifier { get; set; }
    }

}
