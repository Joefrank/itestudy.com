using AutoMapper;
using elearning.model.DataModels;
using elearning.model.ViewModels;
using System;

namespace elearning.admin.Models
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
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

        }
    }
}