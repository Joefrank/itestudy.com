
using elearning.model.DataModels;
using System;

namespace elearning.services.Interfaces
{
    public interface IImageService
    {
        Image GetImage(Guid imageId);

        Image SaveImage(Image image);

        bool DeleteImage(Guid imageId);

        bool DeleteImage(Image image);

        bool IsImageExtension(string ext);

        string AllExtensions();
    }
}
