
using AutoMapper;
using elearning.model.DataModels;
using elearning.model.ViewModels;
using elearning.services.Interfaces;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using elearning.admin.Helpers;

namespace elearning.admin.Controllers
{
    public class CourseController : BaseAdminController
    {
        public ICourseService CourseService { get; set; }
        public ICourseCategoryService CourseCategoryService { get; set; }

        // GET: Courses
        public ActionResult Index()
        {
            var courseList = CourseService.GetAll();
            return View(courseList);
        }

        [HttpGet]
        public ActionResult Create()
        {
           // var categories = CourseCategoryService.GetActiveCategories();

            var model = new CourseEditVm
            {
                CourseCategories = CourseHelper.GetActiveCategoryList()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CourseEditVm model)
        {
            if (!ModelState.IsValid)
            {
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

            return View(model);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var course = CourseService.GetCourse(id);

            var categoryVm = Mapper.Map<Course, CourseEditVm>(course);
            categoryVm.CourseCategories = CourseHelper.GetActiveCategoryList();
            return View(categoryVm);
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult SaveDetails(CourseEditVm model)
        {
            if (!ModelState.IsValid)
            {
                return View("Details", model);
            }

            model.LastModified = DateTime.Now;
            model.LastModifiedBy = CurrentUserDb.Identity;

            //save the details here.
            CourseService.Update(model);

            return Redirect("~/course");
        }

    }
}