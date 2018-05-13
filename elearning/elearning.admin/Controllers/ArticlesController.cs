
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

        [HttpGet]
        public ActionResult Create()
        {           
            return View(GetFileUploadModel(new EditArticleVm()));
        }

        [HttpPost]
        public ActionResult Create(EditArticleVm model)
        {
            if (!ModelState.IsValid)
            {
                return View(GetFileUploadModel(model));
            }

            model.CreatedBy = CurrentUserDb.Identity;
            model.DateCreated = DateTime.Now;

            var article = ArticleService.AddArticle(model);

            if (article != null && article.Id > 0)
                return Redirect("~/articles");

            ModelState.AddModelError("FaileToAddArticle", "Sorry could not add article. Try again or contact support");

            return View(GetFileUploadModel(model));
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var article = ArticleService.GetArticle(id);
            var model = Mapper.Map<Article, EditArticleVm>(article);
           
            return View(GetFileUploadModel(model));
        }

        #region utils

        private EditArticleVm GetFileUploadModel(EditArticleVm articleVm)
        {
            articleVm.FileModel = new FileUploadVm
            {
                MaxFileUpload = 1,
                PopupTitle = "Image Upload",
                ParallelUploads = 1,
                MaxFileSize = 20,
                ImageIdCtrl = "MainImageLink"
            };

            return articleVm;
        }

        #endregion

    }
}