
namespace elearning.model.ViewModels
{
    public class FileUploadVm
    {
        public int MaxFileUpload { get; set; }

        public string PopupTitle { get; set; }

        public int ParallelUploads { get; set; }

        public bool MultipleUpload { get { return MaxFileUpload > 1; } }

        public int MaxFileSize { get; set; }

        public string ImageIdCtrl { get; set; }
    }
}
