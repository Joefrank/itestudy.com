
using AutoMapper;
using elearning.model.DataModels;
using elearning.model.ViewModels;
using elearning.services.Interfaces;
using System;
using System.Web.Mvc;

namespace elearning.admin.Controllers
{
    public class CourseController : BaseAdminController
    {
        public ICourseService CourseService { get; set; }

        // GET: Courses
        public ActionResult Index()
        {
            var courseList = CourseService.GetAll();
            return View(courseList);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(new CourseEditVm());
        }

        [HttpPost]
        public ActionResult Create(CourseEditVm model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            //model.CreatedBy = CurrentUserDb.Identity;
            //model.Status = (int)elearning.model.Enums.CourseCategoryStatus.Published;

            //var category = CourseCategorySvc.AddCategory(model);

            //if (category != null && category.Id > 0)
            //{
            //    return Redirect("~/coursecategory/");
            //}

            //ModelState.AddModelError("Failed Category Add", "Sorry could not add category");
            //model.ShowError = true;
            return View(model);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var course = CourseService.GetCourse(id);

            var categoryVm = Mapper.Map<Course, CourseEditVm>(course);

            return View(categoryVm);
        }

        [HttpPost]
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