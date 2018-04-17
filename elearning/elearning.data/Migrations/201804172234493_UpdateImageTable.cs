namespace elearning.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateImageTable : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Image");
            AlterColumn("dbo.Image", "Identifier", c => c.Guid(nullable: false));
            AddPrimaryKey("dbo.Image", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Image");
            AlterColumn("dbo.Image", "Identifier", c => c.Guid(nullable: false, identity: true));
            AddPrimaryKey("dbo.Image", "Identifier");
        }
    }
}
