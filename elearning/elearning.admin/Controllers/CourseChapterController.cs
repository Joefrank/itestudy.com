
/*****************************************************************
* Code Generated at 31/05/2018 16:59:52
* By Code MVCCodeGenerator
*
*
******************************************************************/
        

using AutoMapper;
using elearning.model.DataModels;
using elearning.model.Enums;
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
            return View();
        }

        public ActionResult GetChapters(int id)
        {
            ViewBag.CourseId = id;
            var chapters = CourseChapterService.GetRelatedCourseChapter(id);
            return View("Chapters", chapters);
        }

        [HttpGet]       
        public ActionResult Create(int id)
        {
            return View(new CourseChapterEditVm {CourseId = id });
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CourseChapterEditVm model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            model.CreatedBy = CurrentUserDb.Identity;
            model.DateCreated = DateTime.Now;
            model.StatusId = (int)CourseChapterStatus.Draft;

            var newChapter = CourseChapterService.AddCourseChapter(model);

            if(newChapter != null && newChapter.Id > 0)
            {
                return Redirect("~/coursechapter/getchapters/" + model.CourseId);
            }

            ModelState.AddModelError("Failed Course chapter Add", "Sorry could not add course chapter.");
            model.ShowError = true;

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
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
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

            return Redirect("~/CourseChapter/details/" + model.Id);
        }
    }
}

