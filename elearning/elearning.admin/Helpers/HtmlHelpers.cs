using elearning.admin.Controllers;
using System.Web.Mvc;

namespace elearning.admin.Helpers
{
    public static class HtmlHelpers
    {
        public static MvcHtmlString CompanySignature(this HtmlHelper helper)
        {
            return new MvcHtmlString(BaseAdminController.CompanySignatureHtmlAdmin);
        }
    }
}