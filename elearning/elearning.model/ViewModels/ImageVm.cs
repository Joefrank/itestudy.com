
using System;

namespace elearning.model.ViewModels
{
    public class ImageVm
    {
        public Guid Identifier { get; set; }
        
        public int Id { get; set; }

        public string Name { get; set; }

        public int Size { get; set; }

        public string Extension { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public string ImageUrl { get; set; }
    }
}
