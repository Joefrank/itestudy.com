using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using elearning.model.Enums;

namespace elearning.model.DataModels
{
    [Table("ArticleCategory")]
    public class ArticleCategory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public int CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? LastModified { get; set; }
        public int? LastModifiedBy { get; set; }
        public ArticleCategoryStatus Status { get; set; }

        public virtual User Creator { get; set; }
        public virtual User LastModifier { get; set; }

        public virtual ICollection<Article> Articles { get; set; }
    }
}
