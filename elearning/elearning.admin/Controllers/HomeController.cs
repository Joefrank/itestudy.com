
using System.Web.Mvc;

namespace elearning.admin.Controllers
{
    public class HomeController : BaseAdminController
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
    }
}