
using AutoMapper;
using elearning.model.DataModels;
using elearning.model.ViewModels;
using elearning.services.Interfaces;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using elearning.admin.Helpers;
using elearning.utils.Helpers;

namespace elearning.admin.Controllers
{
    public class CourseController : BaseAdminController
    {
        public ICourseService CourseService { get; set; }
        public ICourseCategoryService CourseCategoryService { get; set; }
        public IImageService ImageService { get; set; }
        public ICourseChapterService ChapterService { get; set; }

        // GET: Courses
        public ActionResult Index()
        {
            var courseList = CourseService.GetAll();
            return View(courseList);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var model = new CourseEditVm();
            LoadCourseEditModel(model);
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CourseEditVm model)
        {
            if (!ModelState.IsValid)
            {
                LoadCourseEditModel(model);
                return View(model);
            }

            model.CreatedBy = CurrentUserDb.Identity;
            model.DateCreated = DateTime.Now;
            model.Status = (int)elearning.model.Enums.CourseCategoryStatus.Draft;

            var course = CourseService.AddCourse(model);

            if (course != null && course.Id > 0)
            {
                return Redirect("~/course");
            }

            ModelState.AddModelError("Failed Course Add", "Sorry could not add course");
            model.ShowError = true;

            LoadCourseEditModel(model);

            return View(model);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var course = CourseService.GetCourse(id);
            var courseVm = Mapper.Map<Course, CourseEditVm>(course);

            courseVm.CourseChapterCount = course.ChapterCount;

            LoadCourseEditModel(courseVm);
            return View(courseVm);
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult SaveDetails(CourseEditVm model)
        {
            LoadCourseEditModel(model);

            if (!ModelState.IsValid)
            {
                return View("Details", model);
            }

            model.LastModified = DateTime.Now;
            model.LastModifiedBy = CurrentUserDb.Identity;

            //save the details here.
            if (!CourseService.Update(model))
            {                
                ModelState.AddModelError("Failed to save Course details", "Sorry could not save course details!");
                model.ShowError = true;               
            }

            return View("Details", model);
        }

        private void LoadCourseEditModel(CourseEditVm model)
        {
            model.CourseCategories = CourseHelper.GetActiveCategoryList();
            model.FileModel = FileUploadHelper.GetGenericFileUploadModel();
            GetModelImage(model);
        }

        private void GetModelImage(CourseEditVm model)
        {
            if (model.MainImageLink != null && model.MainImageLink != Guid.Empty)
            {
                var image = ImageService.GetImage(model.MainImageLink.Value);
                model.MainImage = Mapper.Map<Image, ImageVm>(image);
            }
        }

    }
}