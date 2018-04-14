
using elearning.data;
using elearning.model.DataModels;
using elearning.services.Interfaces;
using System.Linq;

namespace elearning.services.Implementation
{
    public class ImageService : BaseService, IImageService
    {
        private readonly string[] _validExtensions = { ".jpg", ".bmp", ".gif", ".png" }; //  etc

        public Image SaveImage(Image image)
        {
            //persist user
            using (var context = new DataDbContext())
            {
                context.Images.Add(image);
                context.SaveChanges();
            }

            return image;
        }
        
        public bool IsImageExtension(string ext)
        {
            return _validExtensions.Contains(ext.ToLower());
        }

        public string AllExtensions()
        {
            return string.Join(",", _validExtensions);
        }
    }
}
