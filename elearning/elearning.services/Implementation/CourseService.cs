
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
    public class CourseService : BaseService, ICourseService
    {

        public Course AddCourse(CourseEditVm model)
        {
            try
            {
                var course = Mapper.Map<CourseEditVm, Course>(model);
                course.Creator = UserService.GetUserById(course.CreatedBy);

                using (var context = new DataDbContext())
                {
                    context.Courses.Add(course);
                    context.SaveChanges();
                }

                return course;
            }
            catch (Exception ex)
            {
                Logger.LogItem(ex.Message);
                return null;
            }
        }

        public IEnumerable<Course> FindCourse(string keyowrd)
        {
            try
            {
                IEnumerable<Course> returnList;
                var key = keyowrd.ToLower();

                using (var context = new DataDbContext())
                {
                    returnList = context.Courses.
                        Where(ac => ac.Title.ToLower().Contains(key) 
                        || ac.ShortDescription.ToLower().Contains(key)
                        || ac.Description.ToLower().Contains(key));
                }

                return returnList;
            }
            catch (Exception ex)
            {
                Logger.LogItem(ex.Message);
                return null;
            }
        }

        public List<Course> GetActiveCourses()
        {
            try
            {
                List<Course> returnList;

                using (var context = new DataDbContext())
                {
                    returnList = context.Courses.Include("Creator")
                        .Where(x => x.Status == (int)CourseStatus.Published).ToList();
                }

                return returnList;
            }
            catch (Exception ex)
            {
                Logger.LogItem(ex.Message);
                return null;
            }
        }

        public List<Course> GetAll()
        {
            try
            {
                List<Course> returnList;

                using (var context = new DataDbContext())
                {
                    returnList = context.Courses.Include("Creator").ToList();
                }

                return returnList;
            }
            catch (Exception ex)
            {
                Logger.LogItem(ex.Message);
                return null;
            }
        }

        public List<Course> GetAllSimple()
        {
            try
            {
                List<Course> returnList;

                using (var context = new DataDbContext())
                {
                    returnList = context.Courses.ToList();
                }

                return returnList;
            }
            catch (Exception ex)
            {
                Logger.LogItem(ex.Message);
                return null;
            }
        }

        public Course GetCourse(int courseId)
        {
            Course course=null;

            using (var context = new DataDbContext())
            {
                course = context.Courses.FirstOrDefault(x => x.Id == courseId);
            }

            return course;
        }

        public bool Update(CourseEditVm model)
        {
            try
            {
                using (var context = new DataDbContext())
                {
                    var course = context.Courses.FirstOrDefault(x => x.Id == model.Id);
                    course.Title = model.Title;
                    course.ShortDescription = model.ShortDescription;
                    course.Description = model.Description;
                    course.MainImageLink = model.MainImageLink;
                    course.YoutubeLink = model.YoutubeLink;
                    course.Status = model.Status;
                    course.LastModified = DateTime.Now;
                    course.LastModifiedBy = model.LastModifiedBy;
                    course.LastModifier = context.Users.FirstOrDefault(x => x.Identity == model.LastModifiedBy.Value);

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

        public bool Delete(int courseId)
        {
            try
            {
                using (var context = new DataDbContext())
                {
                    var course = context.Courses.FirstOrDefault(x => x.Id == courseId);
                    context.Courses.Remove(course ?? throw new InvalidOperationException());
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
