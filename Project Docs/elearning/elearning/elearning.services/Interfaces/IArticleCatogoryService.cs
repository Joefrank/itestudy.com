using elearning.model.DataModels;
using elearning.model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace elearning.services.Interfaces
{
    public interface IArticleCatogoryService
    {
        ArticleCatogory AddArticleCategory(AddArticleCategoryVM model);
        //IEnumerable<ArticleCatogory> ListCategory(ListCategoryVM model);
        IEnumerable<ArticleCatogory> FindCategory(String Keyowrd);
        ArticleCatogory GetOneCategory(int CategoryID);
        void update(UpdataArticleCatogoryVM model);
        //void delete(int CategoryID);


    }
}
