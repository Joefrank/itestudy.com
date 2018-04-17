using elearning.services.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;
using elearning.model.DataModels;
using elearning.model.Generic;
using System;
using AutoMapper;
using elearning.model.ViewModels;

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
        public JsonResult UploadImageOnly()
        {
            var returnObject = new JsonResultSet();
            var allImages = new List<ImageVm>();

            try
            {
                foreach (string fileName in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[fileName];
                    var fName = file.FileName;
                    var extension = Path.GetExtension(fName);
                    
                    //validate file uploaded
                    if (file == null || file.ContentLength < 1)
                    {
                        returnObject.IsSuccess = false;
                        returnObject.ErrorMessage = "Invalid or empty file" ;
                        return Json(returnObject, JsonRequestBehavior.AllowGet);
                    }

                    if (!ImageService.IsImageExtension(extension))
                    {
                        returnObject.IsSuccess = false;
                        returnObject.ErrorMessage = "Invalid file extension only Images are accepted. Use one of the following formats: " + ImageService.AllExtensions();
                        return Json(returnObject, JsonRequestBehavior.AllowGet);
                    }
                    //store record into DB
                    var image = PersistImageToDb(file);                        
                   
                    if(image == null || image.Id < 1)
                    {
                        returnObject.IsSuccess = false;
                        returnObject.ErrorMessage = "Sorry but file could not be saved.";
                        return Json(returnObject, JsonRequestBehavior.AllowGet);
                    }

                    var path = Path.Combine(Server.MapPath(ImageUploadDir));
                    var shortName = Path.GetFileName(file.FileName);

                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);

                    var uploadpath = string.Format("{0}\\{1}", path, image.Identifier + image.Extension);
                    file.SaveAs(uploadpath);

                    allImages.Add(Mapper.Map<Image, ImageVm>(image));
                }

                returnObject.IsSuccess = true;
                returnObject.ResultObject = allImages;

                return Json(returnObject, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                LoggerService.LogItem("File upload failed " + ex.Message);
                returnObject.IsSuccess = false;
                returnObject.ErrorMessage = "Sorry! could not save image.";
                return Json(returnObject, JsonRequestBehavior.AllowGet);
            }  
            
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

                    var image = PersistImageToDb(file);

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

        private Image PersistImageToDb(HttpPostedFileBase file)
        {
            var extension = Path.GetExtension(file.FileName);
            var img = System.Drawing.Image.FromStream(file.InputStream, true, true);
            int w = img.Width;
            int h = img.Height;

            var image = new Image
            {
                Identifier = Guid.NewGuid(),
                Name = file.FileName,
                Size = file.ContentLength,
                Extension = extension,
                Width = w,
                Height = h
            };

            return ImageService.SaveImage(image);
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