using System.Web.Mvc;

namespace elearning.utils.Helpers
{
    public static class HtmlHelpers
    {
        public static MvcHtmlString CompanySignature(this HtmlHelper helper, string signature)
        {
            return new MvcHtmlString(signature);
        }
    }
}