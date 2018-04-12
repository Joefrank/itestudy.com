using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace elearning.Controllers
{
    [Route("Articles")]
    public class ArticleController : Controller
    {
        // GET: Article
        public ActionResult Index()
        {
            return View();
        }
        [Route("AddArticles")]
        public ActionResult AddArticle()
        {
            return View();
        }
    }
}