namespace elearning.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update01062018 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CourseChapter", "Creator_Id", c => c.Guid());
            AddColumn("dbo.CourseChapter", "LastModifier_Id", c => c.Guid());
            CreateIndex("dbo.CourseChapter", "Creator_Id");
            CreateIndex("dbo.CourseChapter", "LastModifier_Id");
            AddForeignKey("dbo.CourseChapter", "Creator_Id", "dbo.Users", "Id");
            AddForeignKey("dbo.CourseChapter", "LastModifier_Id", "dbo.Users", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CourseChapter", "LastModifier_Id", "dbo.Users");
            DropForeignKey("dbo.CourseChapter", "Creator_Id", "dbo.Users");
            DropIndex("dbo.CourseChapter", new[] { "LastModifier_Id" });
            DropIndex("dbo.CourseChapter", new[] { "Creator_Id" });
            DropColumn("dbo.CourseChapter", "LastModifier_Id");
            DropColumn("dbo.CourseChapter", "Creator_Id");
        }
    }
}
