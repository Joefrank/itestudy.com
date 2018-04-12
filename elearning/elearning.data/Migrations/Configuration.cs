namespace elearning.data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.IO;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<elearning.data.DataDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "elearning.data.DataDbContext";
        }

        protected override void Seed(elearning.data.DataDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            // Delete all stored procs, views
            //foreach (var file in Directory.GetFiles(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Sql\\Seed"), "*.sql"))
            //{
            //    context.Database.ExecuteSqlCommand(File.ReadAllText(file), new object[0]);
            //}

            //// Add Stored Procedures
            //foreach (var file in Directory.GetFiles(Path.Combine(AppDomain.CurrentDomain.BaseDirectory.Replace(@"\\bin\\debug", ""), "Scripts\\StoredProcs"), "*.sql"))
            //{
            //    context.Database.ExecuteSqlCommand(File.ReadAllText(file), new object[0]);
            //}
        }        

    }
}
