using elearning.services.Interfaces;
using System;
using System.Collections.Generic;
using CYBERMINDS_ELEANING.Model;
using elearning.model.ViewModels;
using elearning.data;

namespace elearning.services.Implementation
{
    public class ArticleService : IArticleService
    {

        // add new article
        public Articles AddArticle(AddArticleVM model)
        {
            var article = Mapper.Map<AddArticleVM, Articles>(model);

            using (var context = new DataDbContext())
            {
                context.Articles.Add(article);
                context.SaveChanges();
            }

            return article;
        }

        public void delete(int CategoryID)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Articles> FindArticle(string Keyowrd)
        {
            throw new NotImplementedException();
        }

        public Articles GetByArticleID(int ArticleID)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Articles> ListArticle(ArticleListVM model)
        {
            throw new NotImplementedException();
        }

        public void update(UpdataArticleVM model)
        {
            throw new NotImplementedException();
        }
    }
}
