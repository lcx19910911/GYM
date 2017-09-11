namespace GYM.DB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admin",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 32, fixedLength: true, unicode: false),
                        Account = c.String(maxLength: 32, unicode: false),
                        Password = c.String(maxLength: 32, unicode: false),
                        Type = c.Int(nullable: false),
                        NickName = c.String(maxLength: 32, unicode: false),
                        HeadImgUrl = c.String(maxLength: 512, unicode: false),
                        Sex = c.Int(nullable: false),
                        MobilePhone = c.String(maxLength: 32, unicode: false),
                        RealName = c.String(maxLength: 32, unicode: false),
                        IsDelete = c.Boolean(nullable: false),
                        CreatedTime = c.DateTime(nullable: false),
                        UpdatedTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 32, fixedLength: true, unicode: false),
                        OpenId = c.String(maxLength: 32, fixedLength: true, unicode: false),
                        NickName = c.String(maxLength: 32, unicode: false),
                        HeadImgUrl = c.String(maxLength: 512, unicode: false),
                        Country = c.String(maxLength: 32, unicode: false),
                        Province = c.String(maxLength: 32, unicode: false),
                        City = c.String(maxLength: 32, unicode: false),
                        Sex = c.Int(nullable: false),
                        MobilePhone = c.String(maxLength: 32, unicode: false),
                        RealName = c.String(maxLength: 32, unicode: false),
                        IsDelete = c.Boolean(nullable: false),
                        CreatedTime = c.DateTime(nullable: false),
                        UpdatedTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.User");
            DropTable("dbo.Admin");
        }
    }
}
