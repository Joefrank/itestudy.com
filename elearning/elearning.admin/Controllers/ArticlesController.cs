
using System;
using System.Web.Mvc;
using AutoMapper;
using elearning.model.DataModels;
using elearning.model.ViewModels;
using elearning.services.Interfaces;

namespace elearning.admin.Controllers
{
    public class ArticlesController : BaseAdminController
    {
        public IArticleService ArticleService { get; set; }

        // GET: Articles
        public ActionResult Index()
        {
            var articles = ArticleService.GetAll();
            return View(articles);
        }

        public ActionResult Create()
        {
            var model = new EditArticleVm();
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(EditArticleVm model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            model.CreatedBy = CurrentUserDb.Identity;
            model.DateCreated = DateTime.Now;

            var article = ArticleService.AddArticle(model);

            if (article != null && article.Id > 0)
                return Redirect("~/articles");
            ModelState.AddModelError("FaileToAddArticle", "Sorry could not add article. Try again or contact support");

            return View(model);
        }

        public ActionResult Details(int id)
        {
            var article = ArticleService.GetArticle(id);
            var model = Mapper.Map<Article, EditArticleVm>(article);

            return View(model);
        }
    }
}