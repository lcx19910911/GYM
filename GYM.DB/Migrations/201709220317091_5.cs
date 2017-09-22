namespace GYM.DB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _5 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Department",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 32, fixedLength: true, unicode: false),
                        ParentID = c.String(maxLength: 32),
                        Name = c.String(nullable: false, maxLength: 32),
                        Sort = c.Int(),
                        Remark = c.String(maxLength: 128),
                        IsDelete = c.Boolean(nullable: false),
                        CreatedTime = c.DateTime(nullable: false),
                        UpdatedTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Menu",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 32, fixedLength: true, unicode: false),
                        Name = c.String(nullable: false, maxLength: 32),
                        Sort = c.Int(),
                        Link = c.String(maxLength: 64),
                        ParentID = c.String(maxLength: 32),
                        ClassName = c.String(maxLength: 32),
                        IsDelete = c.Boolean(nullable: false),
                        CreatedTime = c.DateTime(nullable: false),
                        UpdatedTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Operate",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 32, fixedLength: true, unicode: false),
                        Name = c.String(nullable: false, maxLength: 32),
                        ActionUrl = c.String(nullable: false, maxLength: 64),
                        Sort = c.Int(),
                        Remark = c.String(maxLength: 64),
                        IsDelete = c.Boolean(nullable: false),
                        CreatedTime = c.DateTime(nullable: false),
                        UpdatedTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Admin", "DepartmentID", c => c.String(nullable: false, maxLength: 32));
            AddColumn("dbo.Role", "MenuIDStr", c => c.String(unicode: false, storeType: "text"));
            AddColumn("dbo.Role", "OperateStr", c => c.String(unicode: false, storeType: "text"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Role", "OperateStr");
            DropColumn("dbo.Role", "MenuIDStr");
            DropColumn("dbo.Admin", "DepartmentID");
            DropTable("dbo.Operate");
            DropTable("dbo.Menu");
            DropTable("dbo.Department");
        }
    }
}
