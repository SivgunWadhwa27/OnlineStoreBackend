namespace ShopifyOnlineStoreBackend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCartItemsModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Products", "CartId", "dbo.Carts");
            DropIndex("dbo.Products", new[] { "CartId" });
            CreateTable(
                "dbo.CartItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        Cart_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.Carts", t => t.Cart_Id)
                .Index(t => t.ProductId)
                .Index(t => t.Cart_Id);
            
            DropColumn("dbo.Products", "CartId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "CartId", c => c.Int(nullable: false));
            DropForeignKey("dbo.CartItems", "Cart_Id", "dbo.Carts");
            DropForeignKey("dbo.CartItems", "ProductId", "dbo.Products");
            DropIndex("dbo.CartItems", new[] { "Cart_Id" });
            DropIndex("dbo.CartItems", new[] { "ProductId" });
            DropTable("dbo.CartItems");
            CreateIndex("dbo.Products", "CartId");
            AddForeignKey("dbo.Products", "CartId", "dbo.Carts", "Id", cascadeDelete: true);
        }
    }
}
