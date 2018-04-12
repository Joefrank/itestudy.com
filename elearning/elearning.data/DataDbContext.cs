using elearning.data.Migrations;
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
        public virtual DbSet<Article> Articles { get; set; }
        public virtual DbSet<ArticleCategory> ArticleCategories { get; set; }
        public virtual DbSet<CourseModule> CourseModules { get; set; }
        public virtual DbSet<CourseCategory> CourseCatogories { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<CourseChapter> CourseChapters { get; set; }
        public virtual DbSet<Lesson> lessons { get; set; }
        public virtual DbSet<Log> Logs { get; set; }
        public virtual DbSet<EmailTemplate> EmailTemplates { get; set; }
        public virtual DbSet<Image> Images { get; set; }

        #endregion

    }
}
