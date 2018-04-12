
using System.Web.Mvc;
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

            var article = ArticleService.AddArticle(model);

            if (article != null && article.Id > 0)
                return Redirect("~/articles");
            ModelState.AddModelError("FaileToAddArticle", "Sorry could not add article. Try again or contact support");

            return View(model);
        }
    }
}