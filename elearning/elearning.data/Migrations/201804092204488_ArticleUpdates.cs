namespace elearning.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ArticleUpdates : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Article", "Title", c => c.String(nullable: false, maxLength: 1000));
            AlterColumn("dbo.Article", "YoutubeLinks", c => c.String(maxLength: 200));
            AlterColumn("dbo.Article", "LastModified", c => c.DateTime());
            AlterColumn("dbo.Article", "RelatedObjectId", c => c.Int());
            AlterColumn("dbo.Article", "LastModifiedBy", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Article", "LastModifiedBy", c => c.Int(nullable: false));
            AlterColumn("dbo.Article", "RelatedObjectId", c => c.Int(nullable: false));
            AlterColumn("dbo.Article", "LastModified", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Article", "YoutubeLinks", c => c.String());
            AlterColumn("dbo.Article", "Title", c => c.String(nullable: false));
        }
    }
}
