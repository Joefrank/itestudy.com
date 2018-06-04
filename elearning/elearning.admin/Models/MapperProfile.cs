using AutoMapper;
using elearning.model.DataModels;
using elearning.model.Enums;
using elearning.model.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;

namespace elearning.admin.Models
{
    public class MapperProfile : Profile
    {
       
        public MapperProfile()
        {
            var imageUpload = ConfigurationManager.AppSettings["ImageUploadDir"];
            var siteRoot = ConfigurationManager.AppSettings["BaseUrl"];

            CreateMap<RegisterVm, User>()
                    .ForMember(g => g.Type, opt => opt.MapFrom(source => source.Type.ToString()))
                    .ForMember(g => g.DateRegistered, opt => opt.MapFrom(source => DateTime.Now))
                    .ForMember(g => g.Active, opt => opt.MapFrom(source => false));

            CreateMap<User, UserDetailsVm>()
                .ForMember(g => g.UserIdentity, opt => opt.MapFrom(source => source.Identity));

            CreateMap<ArticleCategoryVm, ArticleCategory>()
                .ForMember(g => g.DateCreated, opt => opt.MapFrom(source => DateTime.Now));

            CreateMap<ArticleCategory, ArticleCategoryDetailsVm>();
            CreateMap<ArticleCategoryDetailsVm, ArticleCategory>();
            CreateMap<EditArticleVm, Article>();
            CreateMap<Article, EditArticleVm>()
                .ForMember(g => g.ArticleId, opt => opt.MapFrom(source => source.Id));

            CreateMap<Image, ImageVm>()
                .ForMember(g => g.ImageUrl, opt => opt.MapFrom(source =>
                    $"{siteRoot}{imageUpload}/{source.Identifier + source.Extension}"));
            CreateMap<CourseCategoryEditVm, CourseCategory>()
                .ForMember(g => g.DateCreated, opt => opt.MapFrom(source => DateTime.Now));
            CreateMap<CourseCategory, CourseCategoryEditVm>();
            CreateMap<Course, CourseEditVm>();
            CreateMap<CourseEditVm, Course>();
            CreateMap<CourseChapter, CourseChapterEditVm>();
            CreateMap<CourseChapterEditVm, CourseChapter>();
            CreateMap<CourseChapter, CourseChapterVm>()
                .ForMember(g => g.Status, opt => opt.MapFrom(source => ((CourseChapterStatus)source.StatusId).ToString()))
                .ForMember(g => g.Creator, opt => opt.MapFrom(source => (source.Creator.Firstname + " " + source.Creator.Lastname)))
                .ForMember(g => g.LastModifier, opt => opt.MapFrom(source => 
                    (source.LastModifier != null? source.LastModifier.Firstname + " " + source.LastModifier.Lastname : "")));

            //CreateMap<IEnumerable<CourseChapter>, IEnumerable<CourseChapterVm>>();

            /***Mapping_Injection***/

        }
    }
}
