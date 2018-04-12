using CYBERMINDS_ELEANING.Model;
using elearning.model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace elearning.services.Interfaces
{
    public interface IArticleService
    {
        Articles AddArticle(AddArticleVM model);
        IEnumerable<Articles> ListArticle(ArticleListVM model);
        IEnumerable<Articles> FindArticle(String Keyowrd);
        Articles GetByArticleID(int ArticleID);
        void update(UpdataArticleVM model);
        void delete(int CategoryID);
    }
}
