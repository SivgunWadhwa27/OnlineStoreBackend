namespace ShopifyOnlineStoreBackend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialSetup : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Carts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Price = c.Double(nullable: false),
                        InventoryCount = c.Int(nullable: false),
                        CartId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Carts", t => t.CartId, cascadeDelete: true)
                .Index(t => t.CartId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "CartId", "dbo.Carts");
            DropIndex("dbo.Products", new[] { "CartId" });
            DropTable("dbo.Products");
            DropTable("dbo.Carts");
        }
    }
}
