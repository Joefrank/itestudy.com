using elearning.services.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;
using elearning.model.DataModels;
using elearning.model.Generic;
using System;

namespace elearning.admin.Controllers
{
    public class FileController : BaseAdminController
    {
        public IImageService ImageService { get; set; }
        
        // GET: File
        public ActionResult Index()
        {
            return View();
        }        

        [HttpPost]
        public JsonResult UploadImage(IEnumerable<HttpPostedFileBase> files)
        {
            var returnObject = new JsonResultSet();

            try
            {
                var imageList = new List<Image>();

                foreach (HttpPostedFileBase file in files)
                {
                    //
                    var extension = Path.GetExtension(file.FileName);

                    if (!ImageService.IsImageExtension(extension))
                    {
                        returnObject.IsSuccess = false;
                        returnObject.ErrorMessage = "Invalid file extension only Images are accepted. Use one of the following formats: " + ImageService.AllExtensions();
                        return Json(returnObject, JsonRequestBehavior.AllowGet);
                    }

                   
                    var img = System.Drawing.Image.FromStream(file.InputStream, true, true);
                    int w = img.Width;
                    int h = img.Height;

                    var image = new Image
                    {
                        Name = file.FileName,
                        Size = file.ContentLength,
                        Extension = extension,
                        Width = w,
                        Height = h
                    };
                    ImageService.SaveImage(image);

                    string filePath = Path.Combine(Server.MapPath(ImageUploadDir), image.Identifier + extension);
                    System.IO.File.WriteAllBytes(filePath, ReadData(file.InputStream));
                    
                    imageList.Add(image);
                }

                returnObject.IsSuccess = true;
                returnObject.ResultObject = imageList;
            }
            catch(Exception ex)
            {
                LoggerService.LogItem(ex.Message);
                returnObject.IsSuccess = false;
                returnObject.ErrorMessage = "Sorry could not save image.";
            }

            return Json(returnObject, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UploadFiles(IEnumerable<HttpPostedFileBase> files)
        {
            foreach (HttpPostedFileBase file in files)
            {
                string filePath = Path.Combine(Server.MapPath(ImageUploadDir), file.FileName);
                System.IO.File.WriteAllBytes(filePath, ReadData(file.InputStream));
            }

            return Json("All files have been successfully stored.");
        }

        private byte[] ReadData(Stream stream)
        {
            byte[] buffer = new byte[16 * 1024];

            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }

                return ms.ToArray();
            }
        }


    }
}