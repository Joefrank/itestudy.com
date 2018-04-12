namespace elearning.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ImagesDetails2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Image", "Identifier", c => c.Guid(nullable: false, identity: true));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Image", "Identifier", c => c.Guid(nullable: false));
        }
    }
}
