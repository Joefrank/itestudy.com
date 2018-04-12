using CYBERMINDS_ELEANING.Model;
using elearning.model.DataModels;
using System.Data.Entity;

namespace elearning.data
{
    public class DataDbContext : DbContext
    {
        public DataDbContext()
             : base("name=elearningConn")
         {
             Configuration.LazyLoadingEnabled = false;
             Configuration.ProxyCreationEnabled = false;
         }
     
        #region Models Declaration 

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Articles> Articles { get; set; }
        public virtual DbSet<ArticleCatogory> ArticleCatogory { get; set; }
        public virtual DbSet<CourseModule> CourseModule { get; set; }
        public virtual DbSet<CourseCatogory> CourseCatogory { get; set; }
        public virtual DbSet<Courses> Courses { get; set; }
        public virtual DbSet<CourseChapter> CourseChapter { get; set; }
        public virtual DbSet<Lessons> lesson { get; set; }
        public virtual DbSet<Log> Logs { get; set; }
        public virtual DbSet<EmailTemplate> EmailTemplates { get; set; }
        #endregion
    }
}
