
using elearning.model.ViewModels;

namespace elearning.utils.Helpers
{
    public static class FileUploadHelper
    {
        public static FileUploadVm GetGenericFileUploadModel()
        {
            return new FileUploadVm
            {
                MaxFileUpload = 1,
                PopupTitle = "Image Upload",
                ParallelUploads = 1,
                MaxFileSize = 20,
                ImageIdCtrl = "MainImageLink"
            };
        }

        public static FileUploadVm GetGenericFileUploadModel(string imgCrl, string popupTitle = "Image Upload", 
            int maxUpload =1, int maxFileSize = 20, int parallelUploads = 1)
        {
            return new FileUploadVm
            {
                MaxFileUpload = 1,
                PopupTitle = popupTitle,
                ParallelUploads = 1,
                MaxFileSize = 20,
                ImageIdCtrl = imgCrl
            };
        }
    }
}
