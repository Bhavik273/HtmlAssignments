namespace ProductManagementWebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initiation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Logins",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Category = c.String(nullable: false),
                        Price = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        ShortDescription = c.String(nullable: false),
                        LongDescription = c.String(),
                        SmallImagePath = c.String(nullable: false),
                        LargeImagePath = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        login_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Logins", t => t.login_Id)
                .Index(t => t.login_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "login_Id", "dbo.Logins");
            DropIndex("dbo.Users", new[] { "login_Id" });
            DropTable("dbo.Users");
            DropTable("dbo.Products");
            DropTable("dbo.Logins");
        }
    }
}
