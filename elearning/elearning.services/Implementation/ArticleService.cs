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
                    articles = context.Articles.ToList();
                    context.SaveChanges();
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
            throw new NotImplementedException();
        }
        
        public void Update(UpdateArticleVm model)
        {
            throw new NotImplementedException();
        }
    }
}
