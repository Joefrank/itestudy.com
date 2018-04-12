using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace elearning.model.DataModels
{
    [Table("CourseCategory")]
    public class CourseCategory
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
        public bool Active { get; set; }

        public virtual User Creator { get; set; }
        public virtual User LastModifier { get; set; }

        public virtual ICollection<Course> course { get; set; }
    }
}
