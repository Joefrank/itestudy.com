using elearning.model.ViewModels;
using elearning.model.DataModels;
using System;
using System.Collections.Generic;

namespace elearning.services.Interfaces
{
    public interface IArticleService
    {
        Article AddArticle(EditArticleVm model);
        List<Article> GetAll();
        IEnumerable<Article> FindArticle(String keyowrd);
        Article GetArticle(int articleId);
        void Update(UpdateArticleVm model);
        void Delete(int categoryId);
    }
}
