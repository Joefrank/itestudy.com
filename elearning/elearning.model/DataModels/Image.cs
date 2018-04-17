
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace elearning.model.DataModels
{
    [Table("Image")]
    public class Image
    { 
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        public Guid Identifier { get; set; }

        public string Name { get; set; }

        public int Size { get; set; }

        public string Extension { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }
    }
}
