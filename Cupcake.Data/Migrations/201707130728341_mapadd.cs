namespace Cupcake.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mapadd : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MapCenters",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                        CreatorId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatorId)
                .Index(t => t.CreatorId);
            
            CreateTable(
                "dbo.MapOverlays",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        MapCenterId = c.Guid(nullable: false),
                        Name = c.String(nullable: false),
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MapOverlays", "CreatorId", "dbo.Users");
            DropForeignKey("dbo.MapCenters", "CreatorId", "dbo.Users");
            DropIndex("dbo.MapOverlays", new[] { "CreatorId" });
            DropIndex("dbo.MapCenters", new[] { "CreatorId" });
            DropTable("dbo.MapOverlays");
            DropTable("dbo.MapCenters");
        }
    }
}
