
/*****************************************************************
* Code Generated at 31/05/2018 16:59:52
* By Code MVCCodeGenerator
*
*
******************************************************************/
        

using elearning.model.DataModels;
using elearning.model.ViewModels;
using System.Collections.Generic;

namespace elearning.services.Interfaces
{
    public interface ICourseChapterService
    {
        CourseChapter AddCourseChapter(CourseChapterEditVm model);
        IEnumerable<CourseChapter> FindCourseChapter(string keyowrd);
        List<CourseChapter> GetActiveCourseChapters();
        List<CourseChapter> GetAll();
        List<CourseChapter> GetAllSimple();
        CourseChapter GetCourseChapter(int courseChapterId);
        bool Update(CourseChapterEditVm model);
        bool Delete(int CourseChapterId);
    }
}
