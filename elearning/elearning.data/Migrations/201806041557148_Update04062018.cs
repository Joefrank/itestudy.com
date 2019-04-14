namespace elearning.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update04062018 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CourseChapter", "DateLastModified", c => c.DateTime());
            AlterColumn("dbo.CourseChapter", "LastModifiedBy", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CourseChapter", "LastModifiedBy", c => c.Int(nullable: false));
            AlterColumn("dbo.CourseChapter", "DateLastModified", c => c.DateTime(nullable: false));
        }
    }
}
