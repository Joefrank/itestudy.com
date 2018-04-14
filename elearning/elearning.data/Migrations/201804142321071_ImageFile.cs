namespace elearning.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ImageFile : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Image");
            AddPrimaryKey("dbo.Image", "Identifier");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Image");
            AddPrimaryKey("dbo.Image", "Id");
        }
    }
}
