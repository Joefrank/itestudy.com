using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using elearning.coding.Models;

namespace elearning.coding.Controllers
{
    public class EditorController : Controller
    {
        // GET: Editor
        public ActionResult Index()
        {
            var initialCode = @" function foo(items) {
                var x = ""All this is syntax highlighted"";
                return x;
            }";

            var editorSettins = new EditorSettingsVm
            {
                InitialCode = initialCode
            };

            return View(editorSettins);
        }
    }
}