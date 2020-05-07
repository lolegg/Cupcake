namespace Cupcake.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class maphy : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MapCenters", "BaseId", c => c.Guid(nullable: false));
            AddColumn("dbo.MapCenters", "ParentId", c => c.Guid());
            AddColumn("dbo.MapCenters", "Longitude", c => c.String());
            AddColumn("dbo.MapCenters", "Latitude", c => c.String());
            AddColumn("dbo.MapCenters", "Type", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.MapCenters", "Type");
            DropColumn("dbo.MapCenters", "Latitude");
            DropColumn("dbo.MapCenters", "Longitude");
            DropColumn("dbo.MapCenters", "ParentId");
            DropColumn("dbo.MapCenters", "BaseId");
        }
    }
}
