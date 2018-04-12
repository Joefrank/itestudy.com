using elearning.services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using elearning.model.DataModels;
using elearning.model.ViewModels;
using elearning.data;
using AutoMapper;
using System;

namespace elearning.services.Implementation
{
    class ArticleCatogoryService : IArticleCatogoryService
    {
        public Dictionary<int, ArticleCatogory> ArticleCategroies = new Dictionary<int, ArticleCatogory>();
        // add new article category
        public ArticleCatogory AddArticleCategory(AddArticleCategoryVM model)
        {
            var category = Mapper.Map<AddArticleCategoryVM, ArticleCatogory>(model);

            using (var context = new DataDbContext())
            {
                context.ArticleCatogory.Add(category);
                context.SaveChanges();
            }

            return category;
        }

        public void delete(int CategoryID)
        {
            ArticleCategroies.Remove(CategoryID);
        }



        public IEnumerable<ArticleCatogory> ListCategory(ListCategoryVM model)
        {
            return ArticleCategroies.Values;
        }

        //Find category

        public IEnumerable<ArticleCatogory> FindCategory(string Keyowrd)
        {
            using (var context = new DataDbContext())
            {
                return context.ArticleCatogory.Where(ac => ac.categoryName.Contains(Keyowrd));
            }

        }


        public ArticleCatogory GetOneCategory(int CategoryID)
        {
            ArticleCatogory category;
            try
            {
                using (var context = new DataDbContext())
                {
                    category = context.ArticleCatogory.FirstOrDefault(ac => ac.categoryID == CategoryID);
                }
                return category;
            }
            catch (Exception e)
            {

                return null;
            }
        }
        


        public void update(UpdataArticleCatogoryVM model)
        {
            

        }

       
    }
}
