namespace elearning.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MainImageLinkChange : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Article", "MainImageLink", c => c.Guid());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Article", "MainImageLink", c => c.Int());
        }
    }
}
