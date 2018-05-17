
using elearning.model.DataModels;
using elearning.model.ViewModels;
using System.Collections.Generic;

namespace elearning.services.Interfaces
{
    public interface ICourseService
    {
        Course AddCourse(CourseEditVm model);
        IEnumerable<Course> FindCourse(string keyowrd);
        List<Course> GetActiveCourses();
        List<Course> GetAll();
        List<Course> GetAllSimple();
        Course GetCourse(int courseId);
        bool Update(CourseEditVm model);
        bool Delete(int courseId);
    }
}
