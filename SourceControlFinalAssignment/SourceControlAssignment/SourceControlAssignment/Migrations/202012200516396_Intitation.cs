namespace SourceControlAssignment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Intitation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserImages",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ImageName = c.String(),
                        Image = c.Binary(),
                        User_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.UserDetails", t => t.User_ID)
                .Index(t => t.User_ID);
            
            CreateTable(
                "dbo.UserDetails",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false),
                        Password = c.String(nullable: false, maxLength: 14),
                        Name = c.String(nullable: false),
                        Contact = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserImages", "User_ID", "dbo.UserDetails");
            DropIndex("dbo.UserImages", new[] { "User_ID" });
            DropTable("dbo.UserDetails");
            DropTable("dbo.UserImages");
        }
    }
}
