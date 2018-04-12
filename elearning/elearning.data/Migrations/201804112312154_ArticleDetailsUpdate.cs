namespace elearning.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ArticleDetailsUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Article", "MainImageLink", c => c.Int());
            AddColumn("dbo.Article", "RelatedObjectTypeId", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Article", "RelatedObjectTypeId");
            DropColumn("dbo.Article", "MainImageLink");
        }
    }
}
