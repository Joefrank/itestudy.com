using elearning.model.ViewModels;
using elearning.services.Interfaces;
using System.Web.Mvc;
using System.Web.Routing;

namespace elearning.Controllers
{
    [Route("ArticleCategory")]
    public class ArticleCategoryController : Controller
    {
        public IArticleCategoryService ArticleCatogoryService { get; set; }
        // GET: ArticleCategory
        public ActionResult Index()
        {
            return View();
        }

        [Route("AddCategory")]
        [HttpPost]
        public ActionResult AddArticleCategory(ArticleCategoryVm model)
        {
            var category = ArticleCatogoryService.AddCategory(model);
            return View(category);
        }

        [Route("AddCategory")]
        [HttpGet]
        public ActionResult AddArticleCategory()
        {
            return View(new ArticleCategoryVm());
        }

        [Route("UpdateCategory")]
        [HttpPut]
        public ActionResult UpdateArticleCategory()
        {
            return View(new ArticleCategoryVm());
        }

        [Route("ArticleList")]
        [HttpGet]
        public ActionResult ListArticleCategory()
        {
            //IArticleCatogoryService.ListCategory();
            return View(new CategoryListVm());
        }
    }
}