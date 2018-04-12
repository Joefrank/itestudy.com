using Autofac.Integration.Mvc;
using elearning.data;
using elearning.Models;
using elearning.utils;
using System.Web.Mvc;
using System.Web.Routing;

namespace elearning
{
    public class MvcApplication : ElearningApplication
    {
        protected void Application_Start()
        {           
            // Set the dependency resolver to be Autofac.
            var container = GetBuilder(typeof(MvcApplication)).Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            //init db context
            ELearningContextCustomInitializer.Initialize();
            //get mapping profiles
            AutoMapper.Mapper.Initialize(prof => prof.AddProfile<MapperProfile>());

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);          

        }
       
    }
}
