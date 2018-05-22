
        /*****************************************************************
        * Code Generated at 22/05/2018 14:27:30
        * By Code MVCCodeGenerator
        *
        *
        ******************************************************************/
        

using AutoMapper;
using elearning.data;
using elearning.model.DataModels;
using elearning.model.Enums;
using elearning.model.ViewModels;
using elearning.services.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace elearning.services.Interfaces
{
    public class CourseChapterService : BaseService, ICourseChapterService
    {

        public CourseChapter AddCourseChapter(CourseChapterEditVm model)
        {
            try
            {
                var CourseChapter = Mapper.Map<CourseChapterEditVm, CourseChapter>(model);
                CourseChapter.Creator = UserService.GetUserById(CourseChapter.CreatedBy);

                using (var context = new DataDbContext())
                {
                    context.CourseChapters.Add(CourseChapter);
                    context.SaveChanges();
                }

                return CourseChapter;
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
                         ac.Description.ToLower().Contains(key) )
                        ;
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
                    returnList = context.CourseChapters.Include("Creator").ToList();//replace with active query
                        //.Where(x => x.Status == (int)CourseChapterStatus.Published).ToList();
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

        public CourseChapter GetCourseChapter(int CourseChapterId)
        {
            CourseChapter CourseChapter=null;

            using (var context = new DataDbContext())
            {
                CourseChapter = context.CourseChapters.FirstOrDefault(x => x.Id == CourseChapterId);
            }

            return CourseChapter;
        }

        public bool Update(CourseChapterEditVm model)
        {
            try
            {
                using (var context = new DataDbContext())
                {
                    var CourseChapter = context.CourseChapters.FirstOrDefault(x => x.Id == model.Id);
                     CourseChapter.Title = model.Title;
 CourseChapter.Description = model.Description;
 CourseChapter.StatusId = model.StatusId;
 CourseChapter.LastModifiedBy = model.LastModifiedBy;
 CourseChapter.LessonCount = model.LessonCount;
 CourseChapter.TutorialCount = model.TutorialCount;


                    context.SaveChanges();
                }

                return true;
            }
            catch(Exception ex)
            {
                Logger.LogItem(ex.Message);
                return false;
            }
        }

        public bool Delete(int CourseChapterId)
        {
            try
            {
                using (var context = new DataDbContext())
                {
                    var CourseChapter = context.CourseChapters.FirstOrDefault(x => x.Id == CourseChapterId);
                    context.CourseChapters.Remove(CourseChapter ?? throw new InvalidOperationException());
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
