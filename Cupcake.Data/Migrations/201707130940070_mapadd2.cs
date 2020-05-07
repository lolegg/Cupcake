namespace Cupcake.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mapadd2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MapOverlays", "Type", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.MapOverlays", "Type");
        }
    }
}
