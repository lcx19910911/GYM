namespace GYM.DB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class store_area : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DataDictionary",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 32, fixedLength: true, unicode: false),
                        ParentKey = c.String(maxLength: 32),
                        GroupCode = c.Int(nullable: false),
                        Key = c.String(maxLength: 32),
                        Value = c.String(nullable: false, maxLength: 32),
                        Remark = c.String(maxLength: 128),
                        Sort = c.Int(),
                        IsDelete = c.Boolean(nullable: false),
                        CreatedTime = c.DateTime(nullable: false),
                        UpdatedTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Store",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 32, fixedLength: true, unicode: false),
                        Name = c.String(nullable: false, maxLength: 32),
                        CityCode = c.String(maxLength: 6),
                        Address = c.String(maxLength: 128),
                        Longitude = c.String(maxLength: 128),
                        Latitude = c.String(maxLength: 128),
                        Introduce = c.String(maxLength: 512),
                        Notice = c.String(maxLength: 256),
                        Pictures = c.String(maxLength: 1024),
                        IsDelete = c.Boolean(nullable: false),
                        CreatedTime = c.DateTime(nullable: false),
                        UpdatedTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            AlterColumn("dbo.Admin", "Account", c => c.String(maxLength: 32));
            AlterColumn("dbo.Admin", "Password", c => c.String(maxLength: 32));
            AlterColumn("dbo.Admin", "NickName", c => c.String(maxLength: 32));
            AlterColumn("dbo.Admin", "HeadImgUrl", c => c.String(maxLength: 512));
            AlterColumn("dbo.Admin", "MobilePhone", c => c.String(maxLength: 32));
            AlterColumn("dbo.Admin", "RealName", c => c.String(maxLength: 32));
            AlterColumn("dbo.User", "OpenId", c => c.String(maxLength: 32));
            AlterColumn("dbo.User", "NickName", c => c.String(maxLength: 32));
            AlterColumn("dbo.User", "HeadImgUrl", c => c.String(maxLength: 512));
            AlterColumn("dbo.User", "Country", c => c.String(maxLength: 32));
            AlterColumn("dbo.User", "Province", c => c.String(maxLength: 32));
            AlterColumn("dbo.User", "City", c => c.String(maxLength: 32));
            AlterColumn("dbo.User", "MobilePhone", c => c.String(maxLength: 32));
            AlterColumn("dbo.User", "RealName", c => c.String(maxLength: 32));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.User", "RealName", c => c.String(maxLength: 32, unicode: false));
            AlterColumn("dbo.User", "MobilePhone", c => c.String(maxLength: 32, unicode: false));
            AlterColumn("dbo.User", "City", c => c.String(maxLength: 32, unicode: false));
            AlterColumn("dbo.User", "Province", c => c.String(maxLength: 32, unicode: false));
            AlterColumn("dbo.User", "Country", c => c.String(maxLength: 32, unicode: false));
            AlterColumn("dbo.User", "HeadImgUrl", c => c.String(maxLength: 512, unicode: false));
            AlterColumn("dbo.User", "NickName", c => c.String(maxLength: 32, unicode: false));
            AlterColumn("dbo.User", "OpenId", c => c.String(maxLength: 32, fixedLength: true, unicode: false));
            AlterColumn("dbo.Admin", "RealName", c => c.String(maxLength: 32, unicode: false));
            AlterColumn("dbo.Admin", "MobilePhone", c => c.String(maxLength: 32, unicode: false));
            AlterColumn("dbo.Admin", "HeadImgUrl", c => c.String(maxLength: 512, unicode: false));
            AlterColumn("dbo.Admin", "NickName", c => c.String(maxLength: 32, unicode: false));
            AlterColumn("dbo.Admin", "Password", c => c.String(maxLength: 32, unicode: false));
            AlterColumn("dbo.Admin", "Account", c => c.String(maxLength: 32, unicode: false));
            DropTable("dbo.Store");
            DropTable("dbo.DataDictionary");
        }
    }
}
