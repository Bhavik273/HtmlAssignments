namespace ProductManagementWebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class catgoryadded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductCategory = c.String(nullable: false, maxLength: 200),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.ProductCategory, unique: true, name: "InxProductCategory");
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Categories", "InxProductCategory");
            DropTable("dbo.Categories");
        }
    }
}
