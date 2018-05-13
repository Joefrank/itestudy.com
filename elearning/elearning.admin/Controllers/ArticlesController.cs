
using System;
using System.Web.Mvc;
using AutoMapper;
using elearning.model.DataModels;
using elearning.model.Enums;
using elearning.model.ViewModels;
using elearning.services.Interfaces;

namespace elearning.admin.Controllers
{
    public class ArticlesController : BaseAdminController
    {
        public IArticleService ArticleService { get; set; }
        public IImageService ImageService { get; set; }

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
            model.Status = (int)ArticleStatus.Draft;

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

            GetModelImage(model);

            return View(GetFileUploadModel(model));
        }

        [HttpPost]
        public ActionResult Update(EditArticleVm model)
        {
            model.LastModifiedBy = CurrentUserDb.Identity;

            if (!ModelState.IsValid || !ArticleService.Update(model))
            {
                ModelState.AddModelError("FaileToSaveArticle", "Sorry could not add article. Please review errors below");
                GetModelImage(model);
                return View(GetFileUploadModel(model));
            }

            return RedirectToAction("Details", new { id = model.ArticleId });
        }

        #region utils

        private void GetModelImage(EditArticleVm model)
        {
            if (model.MainImageLink != null && model.MainImageLink != Guid.Empty)
            {
                var image = ImageService.GetImage(model.MainImageLink.Value);
                model.MainImage = Mapper.Map<Image, ImageVm>(image);
            }
        }

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