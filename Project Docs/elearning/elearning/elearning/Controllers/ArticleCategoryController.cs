using elearning.model.ViewModels;
using elearning.services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace elearning.Controllers
{
    [Route("ArticleCategory")]
    public class ArticleCategoryController : Controller
    {
        public IArticleCatogoryService IArticleCatogoryService { get; set; }
        // GET: ArticleCategory
        public ActionResult Index()
        {
            return View();
        }

        [Route("AddArticleCategory")]
        [HttpPost]
        public ActionResult AddArticleCategory(AddArticleCategoryVM model)
        {
            var category = IArticleCatogoryService.AddArticleCategory(model);
            return View(category);
        }

        [Route("AddArticleCategory")]
        [HttpGet]
        public ActionResult AddArticleCategory()
        {
            return View(new AddArticleCategoryVM());
        }

        [Route("UpdateArticleCategory")]
        [HttpPut]
        public ActionResult UpdateArticleCategory()
        {
            return View(new UpdataArticleCatogoryVM());
        }

        [Route("ArticleCategoryLst")]
        [HttpGet]
        public ActionResult ListArticleCategory()
        {
            
            return View(new ListCategoryVM());
        }
    }
}