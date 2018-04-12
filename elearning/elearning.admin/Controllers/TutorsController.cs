
using System.Web.Mvc;

namespace elearning.admin.Controllers
{
    public class TutorsController : BaseAdminController
    {
        // GET: Tutor
        public ActionResult Index()
        {
            return View();
        }
    }
}