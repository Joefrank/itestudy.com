using System;
using AutoMapper;
using elearning.model.DataModels;
using elearning.model.ViewModels;
using elearning.services.Interfaces;
using System.Web.Mvc;

namespace elearning.admin.Controllers
{
    public class ArticleCategoryController : BaseAdminController
    {
        public IArticleCategoryService CategoryService { get; set; }

        // GET: ArticleCategory
        public ActionResult Index()
        {
            var categories = CategoryService.GetAll();
            return View(categories);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(new ArticleCategoryVm());
        }

        [HttpPost]
        public ActionResult Create(ArticleCategoryVm model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }

            model.CreatedBy = CurrentUserDb.Identity;
            model.Status = elearning.model.Enums.ArticleCategoryStatus.Active;

            var category = CategoryService.AddCategory(model);

            if(category != null && category.Id > 0)
            {
                return Redirect("~/articlecategory/");
            }

            ModelState.AddModelError("Failed Category Add", "Sorry could not add category");
            model.ShowError = true;
            return View(model);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var category = CategoryService.GetCategory(id);

            var categoryVm = Mapper.Map<ArticleCategory, ArticleCategoryDetailsVm> (category);

            return View(categoryVm);
        }

        [HttpPost]
        public ActionResult SaveDetails(ArticleCategoryDetailsVm model)
        {
            if (!ModelState.IsValid)
            {
                return View("Details", model);
            }
            
            model.LastModified = DateTime.Now;
            model.LastModifiedBy = CurrentUserDb.Identity;

            //save the details here.
            CategoryService.Update(model);

            return Redirect("~/articlecategory");
        }
    }
}