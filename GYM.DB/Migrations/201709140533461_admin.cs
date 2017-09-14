namespace GYM.DB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class admin : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Admin", "Mobile", c => c.String(maxLength: 32));
            AddColumn("dbo.Admin", "Discount", c => c.Single(nullable: false));
            AddColumn("dbo.Coach", "HeadImgUrl", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Coach", "Address", c => c.String(maxLength: 256));
            DropColumn("dbo.Admin", "HeadImgUrl");
            DropColumn("dbo.Admin", "MobilePhone");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Admin", "MobilePhone", c => c.String(maxLength: 32));
            AddColumn("dbo.Admin", "HeadImgUrl", c => c.String(maxLength: 512));
            AlterColumn("dbo.Coach", "Address", c => c.String(maxLength: 512));
            DropColumn("dbo.Coach", "HeadImgUrl");
            DropColumn("dbo.Admin", "Discount");
            DropColumn("dbo.Admin", "Mobile");
        }
    }
}
