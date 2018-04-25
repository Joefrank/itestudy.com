
using elearning.data;
using elearning.model.DataModels;
using elearning.services.Interfaces;
using System;
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

        public Image GetImage(Guid imageId)
        {
            Image retImage;

            using (var context = new DataDbContext())
            {
                retImage = context.Images.FirstOrDefault(x => x.Identifier == imageId);               
            }

            return retImage;
        }

        public bool DeleteImage(Guid imageId)
        {
            try
            {
                using (var context = new DataDbContext())
                {
                    var image = context.Images.FirstOrDefault(x => x.Identifier == imageId);
                    if (image != null)
                    {
                        context.Images.Remove(image);
                        context.SaveChanges();
                    }
                }

                return true;
            }
            catch(Exception ex)
            {
                Logger.LogItem("Delete image failed: " + ex.Message);
                return false;
            }
        }

        public bool DeleteImage(Image image)
        {
            try
            {
                using (var context = new DataDbContext())
                {
                    if (image != null)
                    {
                        context.Images.Remove(image);
                        context.SaveChanges();
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                Logger.LogItem("Delete image failed: " + ex.Message);
                return false;
            }
        }

    }
}
