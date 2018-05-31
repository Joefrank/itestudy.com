
/*****************************************************************
* Code Generated at 31/05/2018 16:59:52
* By Code MVCCodeGenerator
*
*
******************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using elearning.data;
using elearning.model.DataModels;
using elearning.model.Enums;
using elearning.model.ViewModels;
using elearning.services.Interfaces;

namespace elearning.services.Implementation
{
    public class CourseChapterService : BaseService, ICourseChapterService
    {
        public CourseChapter AddCourseChapter(CourseChapterEditVm model)
        {
            try
            {
                var courseChapter = Mapper.Map<CourseChapterEditVm, CourseChapter>(model);
                courseChapter.Creator = UserService.GetUserById(courseChapter.CreatedBy);

                using (var context = new DataDbContext())
                {
                    context.CourseChapters.Add(courseChapter);
                    context.SaveChanges();
                }

                return courseChapter;
            }
            catch (Exception ex)
            {
                Logger.LogItem(ex.Message);
                return null;
            }
        }

        public IEnumerable<CourseChapter> FindCourseChapter(string keyowrd)
        {
            try
            {
                IEnumerable<CourseChapter> returnList;
                var key = keyowrd.ToLower();

                using (var context = new DataDbContext())
                {
                    returnList = context.CourseChapters.
                         Where(ac =>  ac.Title.ToLower().Contains(key)  || 
                         ac.Description.ToLower().Contains(key));
                }

                return returnList;
            }
            catch (Exception ex)
            {
                Logger.LogItem(ex.Message);
                return null;
            }
        }

        public List<CourseChapter> GetActiveCourseChapters()
        {
            try
            {
                List<CourseChapter> returnList;

                using (var context = new DataDbContext())
                {
                    returnList = context.CourseChapters
                        .Include("Creator")
                        .Where(x => x.StatusId == (int)CourseChapterStatus.Published).ToList();
                }

                return returnList;
            }
            catch (Exception ex)
            {
                Logger.LogItem(ex.Message);
                return null;
            }
        }

        public List<CourseChapter> GetAll()
        {
            try
            {
                List<CourseChapter> returnList;

                using (var context = new DataDbContext())
                {
                    returnList = context.CourseChapters.Include("Creator").ToList();
                }

                return returnList;
            }
            catch (Exception ex)
            {
                Logger.LogItem(ex.Message);
                return null;
            }
        }

        public List<CourseChapter> GetAllSimple()
        {
            try
            {
                List<CourseChapter> returnList;

                using (var context = new DataDbContext())
                {
                    returnList = context.CourseChapters.ToList();
                }

                return returnList;
            }
            catch (Exception ex)
            {
                Logger.LogItem(ex.Message);
                return null;
            }
        }

        public CourseChapter GetCourseChapter(int courseChapterId)
        {
            CourseChapter courseChapter;

            using (var context = new DataDbContext())
            {
                courseChapter = context.CourseChapters.FirstOrDefault(x => x.Id == courseChapterId);
            }

            return courseChapter;
        }

        public bool Update(CourseChapterEditVm model)
        {
            try
            {
                using (var context = new DataDbContext())
                {
                    var courseChapter = context.CourseChapters.FirstOrDefault(x => x.Id == model.Id);

                    if (courseChapter != null)
                    {
                        courseChapter.Title = model.Title;
                        courseChapter.Description = model.Description;
                        courseChapter.StatusId = model.StatusId;
                        courseChapter.LastModifiedBy = model.LastModifiedBy;
                        courseChapter.LessonCount = model.LessonCount;
                        courseChapter.TutorialCount = model.TutorialCount;

                        context.SaveChanges();
                    }
                }

                return true;
            }
            catch(Exception ex)
            {
                Logger.LogItem(ex.Message);
                return false;
            }
        }

        public bool Delete(int courseChapterId)
        {
            try
            {
                using (var context = new DataDbContext())
                {
                    var courseChapter = context.CourseChapters.FirstOrDefault(x => x.Id == courseChapterId);
                    context.CourseChapters.Remove(courseChapter ?? throw new InvalidOperationException());
                    context.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                Logger.LogItem(ex.Message);
                return false;
            }
        }
    }
}
