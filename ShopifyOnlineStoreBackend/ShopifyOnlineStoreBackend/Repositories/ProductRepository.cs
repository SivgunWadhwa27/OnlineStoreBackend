using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShopifyOnlineStoreBackend.Models;

namespace OnlineStoreBackend.Repositories
{
    public class ProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Product GetProduct(int productId)
        {
            //Get the product from the Database context
            //matching on the productId
            var product = _context.Products.SingleOrDefault(p => p.Id == productId);

            return product;
        }

        public List<Product> GetAllProducts()
        {
            //Get all products from the Database context
            var allProducts = _context.Products.ToList();

            return allProducts;
        }

        public List<Product> GetProductsWithAvailableInventory()
        {
            var availableProducts = new List<Product>();

            //Loop through all products and if in inventory, then add to the list
            foreach (var product in _context.Products)
            {
                if (product.InventoryCount > 0)
                {
                    availableProducts.Add(product);
                }
            }

            return availableProducts;
        }

    }
}