

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using elearning.model.ViewModels;
using elearning.services.Interfaces;

namespace elearning.admin.Helpers
{
    public class CourseHelper
    {
        private static ICourseCategoryService CategoryService =>
            DependencyResolver.Current.GetService<ICourseCategoryService>();

        private static IImageService ImageSvc =>
            DependencyResolver.Current.GetService<IImageService>();

        public static List<GlossaryVm> GetActiveCategoryList()
        {
            var allCategories = CategoryService.GetActiveCategories();
            return allCategories.Select(x => new GlossaryVm { Id = x.Id, Name = x.Name }).ToList();
        }

        public static List<GlossaryVm> GetCourseChapterStatus()
        {
            var allCategories = CategoryService.GetActiveCategories();
            return allCategories.Select(x => new GlossaryVm { Id = x.Id, Name = x.Name }).ToList();
        }


        public static string GetCourseMainImage(Guid? imageId)
        {
            if(!imageId.HasValue)
                return string.Empty;

            var image = ImageSvc.GetImage(imageId.Value);

            if (image == null)
                return string.Empty;

            return image.Identifier + "." + image.Extension;
        }
       

    }
}