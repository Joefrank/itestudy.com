using elearning.services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using elearning.model.ViewModels;
using elearning.data;
using AutoMapper;
using elearning.model.DataModels;

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
            catch (Exception ex)
            {
                Logger.LogItem(ex.Message);
                return null;
            }
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
        
        public void Update(UpdateArticleVm model)
        {
            throw new NotImplementedException();
        }
    }
}
