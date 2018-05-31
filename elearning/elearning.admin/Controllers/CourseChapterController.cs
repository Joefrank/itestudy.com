
/*****************************************************************
* Code Generated at 31/05/2018 16:59:52
* By Code MVCCodeGenerator
*
*
******************************************************************/
        

using AutoMapper;
using elearning.model.DataModels;
using elearning.model.ViewModels;
using elearning.services.Interfaces;
using System;
using System.Web.Mvc;

namespace elearning.admin.Controllers
{
    public class CourseChapterController : BaseAdminController
    {
        public ICourseChapterService CourseChapterService { get; set; }

        // GET: CourseChapters
        public ActionResult Index()
        {
            var courseChapterList = CourseChapterService.GetAll();
            return View(courseChapterList);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(new CourseChapterEditVm());
        }

        [HttpPost]
        public ActionResult Create(CourseChapterEditVm model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            model.CreatedBy = CurrentUserDb.Identity;
            model.DateCreated = DateTime.Now;

            return View(model);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var courseChapter = CourseChapterService.GetCourseChapter(id);

            var courseChapterVm = Mapper.Map<CourseChapter, CourseChapterEditVm>(courseChapter);

            return View(courseChapterVm);
        }

        [HttpPost]
        public ActionResult SaveDetails(CourseChapterEditVm model)
        {
            if (!ModelState.IsValid)
            {
                return View("Details", model);
            }

            model.DateLastModified = DateTime.Now;
            model.LastModifiedBy = CurrentUserDb.Identity;

            //save the details here.
            CourseChapterService.Update(model);

            return Redirect("~/CourseChapter");
        }
    }
}

