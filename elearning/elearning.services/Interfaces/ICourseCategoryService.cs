
using elearning.model.DataModels;
using elearning.model.ViewModels;
using System.Collections.Generic;

namespace elearning.services.Interfaces
{
    public interface ICourseCategoryService
    {
        CourseCategory AddCategory(CourseCategoryEditVm model);
        IEnumerable<CourseCategory> FindCategory(string keyowrd);
        List<CourseCategory> GetActiveCategories();
        List<CourseCategory> GetAll();
        CourseCategory GetCategory(int categoryId);
        bool Update(CourseCategoryEditVm model);
        bool Delete(int categoryId);
    }
}
