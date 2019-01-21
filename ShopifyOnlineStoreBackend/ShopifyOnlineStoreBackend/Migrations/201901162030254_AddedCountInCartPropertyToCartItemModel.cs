namespace ShopifyOnlineStoreBackend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCountInCartPropertyToCartItemModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CartItems", "CountInCart", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CartItems", "CountInCart");
        }
    }
}
