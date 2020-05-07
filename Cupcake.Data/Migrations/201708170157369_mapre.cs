namespace Cupcake.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mapre : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MapCenters", "CreatorId", "dbo.Users");
            DropIndex("dbo.MapCenters", new[] { "CreatorId" });
            CreateTable(
                "dbo.MapOverlayPoints",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        MapOverlayId = c.Guid(nullable: false),
                        Longitude = c.String(nullable: false),
                        Latitude = c.String(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                        CreatorId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatorId)
                .Index(t => t.CreatorId);
            
            AddColumn("dbo.MapOverlays", "ParentId", c => c.Guid());
            AddColumn("dbo.MapOverlays", "Level", c => c.Int(nullable: false));
            AddColumn("dbo.MapOverlays", "Color", c => c.String());
            AlterColumn("dbo.MapOverlays", "Longitude", c => c.String());
            AlterColumn("dbo.MapOverlays", "Latitude", c => c.String());
            DropColumn("dbo.MapOverlays", "MapCenterId");
            DropColumn("dbo.MapOverlays", "Type");
            DropTable("dbo.MapCenters");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.MapCenters",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        BaseId = c.Guid(nullable: false),
                        ParentId = c.Guid(),
                        Longitude = c.String(),
                        Latitude = c.String(),
                        Type = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                        CreatorId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.MapOverlays", "Type", c => c.String());
            AddColumn("dbo.MapOverlays", "MapCenterId", c => c.Guid(nullable: false));
            DropForeignKey("dbo.MapOverlayPoints", "CreatorId", "dbo.Users");
            DropIndex("dbo.MapOverlayPoints", new[] { "CreatorId" });
            AlterColumn("dbo.MapOverlays", "Latitude", c => c.String(nullable: false));
            AlterColumn("dbo.MapOverlays", "Longitude", c => c.String(nullable: false));
            DropColumn("dbo.MapOverlays", "Color");
            DropColumn("dbo.MapOverlays", "Level");
            DropColumn("dbo.MapOverlays", "ParentId");
            DropTable("dbo.MapOverlayPoints");
            CreateIndex("dbo.MapCenters", "CreatorId");
            AddForeignKey("dbo.MapCenters", "CreatorId", "dbo.Users", "Id");
        }
    }
}
