namespace ShopifyOnlineStoreBackend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCompletePropertyToCartModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Carts", "Complete", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Carts", "Complete");
        }
    }
}
