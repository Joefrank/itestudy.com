using elearning.model.ViewModels;
using elearning.model.DataModels;
using System;
using System.Collections.Generic;

namespace elearning.services.Interfaces
{
    public interface ICourseCatogoryService
    {
        ArticleCategory AddCategory(ArticleCategoryVm model);       
        IEnumerable<ArticleCategory> FindCategory(string keyowrd);
        List<ArticleCategory> GetActiveCategories();
        List<ArticleCategory> GetAll();
        ArticleCategory GetCategory(int categoryId);
        void Update(ArticleCategoryDetailsVm model);
        void Delete(int categoryId);
    }
}
