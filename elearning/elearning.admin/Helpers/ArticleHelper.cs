﻿using elearning.model.Enums;
using elearning.model.ViewModels;
using elearning.services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace elearning.admin.Helpers
{
    public class ArticleHelper
    {
        public static ICourseCatogoryService CategoryService { get { return DependencyResolver.Current.GetService<ICourseCatogoryService>(); } }

        public static List<GlossaryVm> GetCategoryList()
        {
            var allCategories = CategoryService.GetActiveCategories();
            return allCategories.Select(x => new GlossaryVm { Id = x.Id, Name = x.Name }).ToList();
        }

        public static List<GlossaryVm> GetArticleTypeList()
        {
            var returnList = new List<GlossaryVm>();

            foreach (ArticleType atype in Enum.GetValues(typeof(ArticleType)))
            {
                returnList.Add(new GlossaryVm { Id = (int)atype, Name = atype.ToString() });
            }
            return returnList;            
        }

        public static List<GlossaryVm> GetArticleRelatedTypeList()
        {
            var returnList = new List<GlossaryVm>();

            foreach (ArticleRelatedObjectType atype in Enum.GetValues(typeof(ArticleRelatedObjectType)))
            {
                returnList.Add(new GlossaryVm { Id = (int)atype, Name = atype.ToString() });
            }
            return returnList;
        }

       

        public static List<GlossaryVm> GetGlossaryList<T>() where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("T must be an enumerated type");
            }

            var returnList = new List<GlossaryVm>();

            foreach (var atype in Enum.GetValues(typeof(T)))
            {
                returnList.Add(new GlossaryVm { Id = (int)atype, Name = atype.ToString() });
            }

            return returnList;
        }
    }
}