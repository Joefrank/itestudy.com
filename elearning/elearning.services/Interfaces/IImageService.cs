
using elearning.model.DataModels;

namespace elearning.services.Interfaces
{
    public interface IImageService
    {
        Image SaveImage(Image image);

        bool IsImageExtension(string ext);

        string AllExtensions();
    }
}
