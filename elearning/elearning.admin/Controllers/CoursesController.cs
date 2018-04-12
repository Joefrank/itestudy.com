
using System.Web.Mvc;

namespace elearning.admin.Controllers
{
    public class CoursesController : BaseAdminController
    {
        // GET: Courses
        public ActionResult Index()
        {
            return View();
        }
    }
}