

using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using elearning.model.ViewModels;
using elearning.services.Interfaces;

namespace elearning.admin.Helpers
{
    public class CourseHelper
    {
        public static ICourseCategoryService CategoryService =>
            DependencyResolver.Current.GetService<ICourseCategoryService>();

        public static List<GlossaryVm> GetActiveCategoryList()
        {
            var allCategories = CategoryService.GetActiveCategories();
            return allCategories.Select(x => new GlossaryVm { Id = x.Id, Name = x.Name }).ToList();
        }
    }
}