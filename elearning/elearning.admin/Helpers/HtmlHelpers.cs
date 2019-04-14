using elearning.admin.Controllers;
using elearning.model.ViewModels;
using elearning.services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
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

        public static MvcHtmlString MakeBreadCrumb(this HtmlHelper helper, IEnumerable<Crumb> crumbs)
        {
            var mainStr = @"<div class=""col-md-8"">
                                <ol class=""breadcrumb"">
                                    <li class=""breadcrumb-item active"" title=""Go to Home Page""><a href=""/"">Home</a></li>
                                    {0}                                   
                                </ol>
                            </div>";

            var crumbTemplate = @"<li class=""breadcrumb-item{0}"">{1}</li>";
            var sbTemp = new StringBuilder();

            foreach (var crumb in crumbs)
            {
                var activeStr = string.Empty;
                var lnkStr = string.Empty;

                if (!string.IsNullOrEmpty(crumb.Url))
                {
                    activeStr = " active ";
                    lnkStr = string.Format("<a href=\"{0}\">{1}</a>", crumb.Url, crumb.Label);
                }
                else
                {
                    lnkStr = crumb.Label;
                }

                sbTemp.AppendLine(string.Format(crumbTemplate, activeStr, lnkStr));
            }

            return new MvcHtmlString(string.Format(mainStr, sbTemp.ToString()));

        }
    }
}