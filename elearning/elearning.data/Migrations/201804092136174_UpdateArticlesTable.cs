namespace elearning.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateArticlesTable : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Articles", newName: "Article");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Article", newName: "Articles");
        }
    }
}
