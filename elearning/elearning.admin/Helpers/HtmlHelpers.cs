using elearning.admin.Controllers;
using elearning.services.Interfaces;
using System;
using System.Web.Mvc;

namespace elearning.admin.Helpers
{
    public static class HtmlHelpers
    {
        private static IImageService ImageSvc =>
           DependencyResolver.Current.GetService<IImageService>();


        public static MvcHtmlString CompanySignature(this HtmlHelper helper)
        {
            return new MvcHtmlString(BaseAdminController.CompanySignatureHtmlAdmin);
        }

        public static MvcHtmlString GetCourseMainImageHtml(this HtmlHelper helper, Guid? imageId, string className, string imgPath, string alt)
        {
            if (!imageId.HasValue)
                return MvcHtmlString.Empty;

            var image = ImageSvc.GetImage(imageId.Value);

            if (image == null)
                return MvcHtmlString.Empty;

            return new MvcHtmlString(string.Format("<img class=\"{2}\" src=\"{0}\" alt=\"{1}\" />", 
                imgPath + "/" + image.Identifier + image.Extension, alt, className));
        }
    }
}