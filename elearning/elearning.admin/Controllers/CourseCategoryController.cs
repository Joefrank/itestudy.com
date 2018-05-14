
using AutoMapper;
using elearning.model.DataModels;
using elearning.model.ViewModels;
using elearning.services.Interfaces;
using System;
using System.Web.Mvc;

namespace elearning.admin.Controllers
{
    public class CourseCategoryController : BaseAdminController
    {
        public ICourseCategoryService CourseCategorySvc { get; set; }

        // GET: CourseCategory
        public ActionResult Index()
        {
            var categories = CourseCategorySvc.GetAll();
            return View(categories);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(new CourseCategoryEditVm());
        }

        [HttpPost]
        public ActionResult Create(CourseCategoryEditVm model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            model.CreatedBy = CurrentUserDb.Identity;
            model.Status = (int)elearning.model.Enums.CourseCategoryStatus.Published;

            var category = CourseCategorySvc.AddCategory(model);

            if (category != null && category.Id > 0)
            {
                return Redirect("~/coursecategory/");
            }

            ModelState.AddModelError("Failed Category Add", "Sorry could not add category");
            model.ShowError = true;
            return View(model);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var category = CourseCategorySvc.GetCategory(id);

            var categoryVm = Mapper.Map<CourseCategory, CourseCategoryEditVm>(category);

            return View(categoryVm);
        }

        [HttpPost]
        public ActionResult SaveDetails(CourseCategoryEditVm model)
        {
            if (!ModelState.IsValid)
            {
                return View("Details", model);
            }

            model.LastModified = DateTime.Now;
            model.LastModifiedBy = CurrentUserDb.Identity;

            //save the details here.
            CourseCategorySvc.Update(model);

            return Redirect("~/coursecategory");
        }
    }
}