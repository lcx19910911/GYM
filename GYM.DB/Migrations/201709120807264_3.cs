namespace GYM.DB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _3 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Store", "Longitude");
            DropColumn("dbo.Store", "Latitude");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Store", "Latitude", c => c.String(maxLength: 128));
            AddColumn("dbo.Store", "Longitude", c => c.String(maxLength: 128));
        }
    }
}
