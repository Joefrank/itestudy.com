using AutoMapper;
using elearning.data;
using elearning.model.DataModels;
using elearning.model.Enums;
using elearning.model.ViewModels;
using elearning.services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace elearning.services.Implementation
{
    public class CourseCategoryService : BaseService, ICourseCategoryService
    {
        public CourseCategory AddCategory(CourseCategoryEditVm model)
        {
            try
            {
                var category = Mapper.Map<CourseCategoryEditVm, CourseCategory>(model);

                category.Creator = UserService.GetUserById(category.CreatedBy);

                using (var context = new DataDbContext())
                {
                    context.CourseCatogories.Add(category);
                    context.SaveChanges();
                }

                return category;
            }
            catch (Exception ex)
            {
                Logger.LogItem(ex.Message);
                return null;
            }
        }

        public IEnumerable<CourseCategory> FindCategory(string keyowrd)
        {
            try
            {

                IEnumerable<CourseCategory> returnList;

                using (var context = new DataDbContext())
                {
                    returnList = context.CourseCatogories.
                        Where(ac => ac.Name.ToLower().Contains(keyowrd.ToLower()));
                }

                return returnList;
            }
            catch (Exception ex)
            {
                Logger.LogItem(ex.Message);
                return null;
            }
        }

        public List<CourseCategory> GetActiveCategories()
        {
            try
            {
                List<CourseCategory> returnList;

                using (var context = new DataDbContext())
                {
                    returnList = context.CourseCatogories.Include("Creator")
                        .Where(x => x.Status == (int)CourseCategoryStatus.Published).ToList();
                }

                return returnList;
            }
            catch (Exception ex)
            {
                Logger.LogItem(ex.Message);
                return null;
            }
        }

        public List<CourseCategory> GetAll()
        {
            try
            {
                List<CourseCategory> returnList;

                using (var context = new DataDbContext())
                {
                    returnList = context.CourseCatogories
                        .Include("Creator").ToList();
                }

                return returnList;
            }
            catch (Exception ex)
            {
                Logger.LogItem(ex.Message);
                return null;
            }
        }

        public CourseCategory GetCategory(int categoryId)
        {
            CourseCategory category;

            try
            {
                using (var context = new DataDbContext())
                {
                    category = context.CourseCatogories.FirstOrDefault(ac => ac.Id == categoryId);

                    if (category != null)
                    {
                        category.Creator = context.Users.FirstOrDefault(x => x.Identity == category.CreatedBy);
                        if (category.LastModifiedBy.HasValue)
                        {
                            category.LastModifier =
                                context.Users.FirstOrDefault(x => x.Identity == category.LastModifiedBy.Value);
                        }
                    }


                }
                return category;
            }
            catch (Exception ex)
            {
                Logger.LogItem(ex.Message);
                return null;
            }
        }

        public bool Update(CourseCategoryEditVm model)
        {
            try
            {
                using (var context = new DataDbContext())
                {
                    var category = context.CourseCatogories.FirstOrDefault(x => x.Id == model.Id);

                    if (category == null)
                        throw new Exception("Course category not found " + model.Id);

                    category.Name = model.Name;
                    category.Description = model.Description;
                    category.LastModified = DateTime.Now;
                    category.LastModifiedBy = model.LastModifiedBy;
                    category.Status = model.Status;
                    category.LastModifier = context.Users.FirstOrDefault(x => x.Identity == model.LastModifiedBy);

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

        public bool Delete(int categoryId)
        {
            try
            {
                using (var context = new DataDbContext())
                {
                    var category = context.ArticleCategories.FirstOrDefault(x => x.Id == categoryId);
                    context.ArticleCategories.Remove(category);
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
