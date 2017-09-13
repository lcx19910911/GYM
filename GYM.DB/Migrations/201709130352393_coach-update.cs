namespace GYM.DB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class coachupdate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Coach",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 32, fixedLength: true, unicode: false),
                        Name = c.String(nullable: false, maxLength: 32),
                        IDCard = c.String(nullable: false, maxLength: 32),
                        Address = c.String(maxLength: 512),
                        Sex = c.Int(nullable: false),
                        Mobile = c.String(maxLength: 11),
                        BasicSalary = c.Decimal(precision: 18, scale: 2),
                        TrainYears = c.Int(),
                        EntryDate = c.DateTime(),
                        Introduce = c.String(maxLength: 512),
                        Pictures = c.String(maxLength: 1024),
                        StoreID = c.String(nullable: false, maxLength: 32),
                        QuitTime = c.DateTime(),
                        IsQuit = c.Boolean(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                        CreatedTime = c.DateTime(nullable: false),
                        UpdatedTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Admin", "CoachID", c => c.String(maxLength: 32));
            DropColumn("dbo.Admin", "NickName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Admin", "NickName", c => c.String(maxLength: 32));
            DropColumn("dbo.Admin", "CoachID");
            DropTable("dbo.Coach");
        }
    }
}
