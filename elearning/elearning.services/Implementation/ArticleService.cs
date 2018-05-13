using elearning.services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using elearning.model.ViewModels;
using elearning.data;
using AutoMapper;
using elearning.model.DataModels;
using System.Data.Entity.Validation;
using System.Text;

namespace elearning.services.Implementation
{
    public class ArticleService : BaseService, IArticleService
    {
        // add new article
        public Article AddArticle(EditArticleVm model)
        {
            try
            {
                var article = Mapper.Map<EditArticleVm, Article>(model);

                using (var context = new DataDbContext())
                {
                    article.Creator = UserService.GetUserById(article.CreatedBy);

                    context.Articles.Add(article);
                    context.SaveChanges();
                }

                return article;
            }
            catch(DbEntityValidationException dbEx)
            {
                var sbContent = new StringBuilder();

                foreach (var eve in dbEx.EntityValidationErrors)
                {
                    sbContent.AppendLine(string.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State));

                    foreach (var ve in eve.ValidationErrors)
                    {
                        sbContent.AppendLine(string.Format("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage));
                    }
                }

                Logger.LogItem(sbContent.ToString());                
            }
            catch (Exception ex)
            {
                Logger.LogItem(ex.Message);
            }

            return null;
        }

        public List<Article> GetAll()
        {
            try
            {
                List<Article> articles = null;

                using (var context = new DataDbContext())
                {
                    articles = context.Articles
                        .Include("Creator").ToList();
                }

                return articles;
            }
            catch (Exception ex)
            {
                Logger.LogItem(ex.Message);
                return null;
            }

        }

        public void Delete(int articleId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Article> FindArticle(string keyowrd)
        {
            throw new NotImplementedException();
        }

        public Article GetArticle(int articleId)
        {
            Article retArticle;

            using (var context = new DataDbContext())
            {
                retArticle = context.Articles.FirstOrDefault(x => x.Id == articleId);
                if(retArticle.Creator == null)
                    retArticle.Creator = UserService.GetUserById(retArticle.CreatedBy);
            }

            return retArticle;
        }
        
        public bool Update(EditArticleVm model)
        {
            using (var context = new DataDbContext())
            {
                var retArticle = context.Articles.FirstOrDefault(x => x.Id == model.ArticleId);

                retArticle.LastModified = DateTime.Now;
                retArticle.LastModifiedBy = model.LastModifiedBy;
                retArticle.Title = model.Title;
                retArticle.Content = model.Content;
                retArticle.Status = model.Status;
                retArticle.MainImageLink = model.MainImageLink;
                retArticle.CategoryId = model.CategoryId;
                retArticle.Type = model.Type;
                retArticle.YoutubeLinks = model.YoutubeLinks;
                retArticle.RelatedObjectId = model.RelatedObjectId;
                retArticle.RelatedObjectTypeId = model.RelatedObjectTypeId;

                context.SaveChanges();
            }

            return true;
        }
    }
}
