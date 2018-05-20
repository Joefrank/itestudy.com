
        /*****************************************************************
        * Code Generated at 20/05/2018 23:45:39
        * By Code MVCCodeGenerator
        *
        *
        ******************************************************************/
        

using elearning.model.DataModels;
using elearning.model.ViewModels;
using System;
using System.Collections;
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
        CourseChapter GetCourseChapter(int CourseChapterId);
        bool Update(CourseChapterEditVm model);
        bool Delete(int CourseChapterId);
    }
}
