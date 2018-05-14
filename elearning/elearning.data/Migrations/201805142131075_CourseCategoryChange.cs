namespace elearning.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CourseCategoryChange : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CourseCategory", "Status", c => c.Int(nullable: false));
            DropColumn("dbo.CourseCategory", "Active");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CourseCategory", "Active", c => c.Boolean(nullable: false));
            DropColumn("dbo.CourseCategory", "Status");
        }
    }
}
