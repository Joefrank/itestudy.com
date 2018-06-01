using elearning.services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using elearning.model.ViewModels;
using AutoMapper;
using elearning.data;
using elearning.model.DataModels;

namespace elearning.services.Implementation
{
    public class ArticleCategoryService : BaseService, IArticleCategoryService
    {
       
        // add new article category
        public ArticleCategory AddCategory(ArticleCategoryVm model)
        {
            try
            {
                var category = Mapper.Map<ArticleCategoryVm, ArticleCategory>(model);

                category.Creator = UserService.GetUserById(category.CreatedBy);

                using (var context = new DataDbContext())
                {
                    context.ArticleCategories.Add(category);
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
       
        
        public IEnumerable<ArticleCategory> FindCategory(string keyowrd)
        {
            try { 

                IEnumerable<ArticleCategory> returnList;

                using (var context = new DataDbContext())
                {
                    returnList = context.ArticleCategories.Where(ac => ac.Name.ToLower().Contains(keyowrd.ToLower()));
                }

                return returnList;
            }
            catch (Exception ex)
            {
                Logger.LogItem(ex.Message);
                return null;
            }
        }       

        //list category
        public List<ArticleCategory> GetAll()
        {
            try { 
                List<ArticleCategory> returnList;

                using (var context = new DataDbContext())
                {
                    returnList = context.ArticleCategories
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

        public List<ArticleCategory> GetActiveCategories()
        {
            try
            {
                List<ArticleCategory> returnList;

                using (var context = new DataDbContext())
                {
                    returnList = context.ArticleCategories.Where(x => x.Status == model.Enums.ArticleCategoryStatus.Active)
                       .ToList();
                }

                return returnList;
            }
            catch (Exception ex)
            {
                Logger.LogItem(ex.Message);
                return null;
            }
        }

        public ArticleCategory GetCategory(int categoryId)
        {
            ArticleCategory category;

            try
            {
                using (var context = new DataDbContext())
                {
                    category = context.ArticleCategories.FirstOrDefault(ac => ac.Id == categoryId);

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

        public void Update(ArticleCategoryDetailsVm model)
        {
            try
            {
                using (var context = new DataDbContext())
                {
                    var category = context.ArticleCategories.FirstOrDefault(x => x.Id == model.Id);
                    if(category == null)
                        throw new Exception("Article category not found " + model.Id);

                    category.Name = model.Name;
                    category.Description = model.Description;
                    category.LastModified = DateTime.Now;
                    category.LastModifiedBy = model.LastModifiedBy;
                    category.Status = model.Status;
                    category.LastModifier = context.Users.FirstOrDefault(x => x.Identity == model.LastModifiedBy);

                    context.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                Logger.LogItem(ex.Message);
            }

        }

        public void Delete(int categoryId)
        {
            try { 
                using (var context = new DataDbContext())
                {
                    var category = context.ArticleCategories.FirstOrDefault(x => x.Id == categoryId);
                    context.ArticleCategories.Remove(category ?? throw new InvalidOperationException());
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Logger.LogItem(ex.Message);
            }
        }

    }
}
