using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using ShopifyOnlineStoreBackend.Models;
using ShopifyOnlineStoreBackend.ViewModels;

namespace OnlineStoreBackend.Repositories
{
    public class CartRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ProductRepository ProductRepository;

        public CartRepository(ApplicationDbContext context)
        {
            _context = context;
            ProductRepository = new ProductRepository(_context);
        }

        public Cart GetCurrentCart()
        {
            //Fetch the current cart, which means its boolean property complete should be false
            var cart = _context.Carts
                .Include("CartItems")
                .SingleOrDefault(c => !c.Complete);

            //If cart does not exist then create a new one
            if (cart == null)
            {
                cart = new Cart();
                Add(cart);
            }

            return cart;
        }

        public void Add(Cart cart)
        {
            _context.Carts.Add(cart);
            _context.SaveChanges();
        }

        public bool ProductExistsInCart(Cart cart, int productId)
        {
            foreach (var cartItem in cart.CartItems)
            {
                if (cartItem.ProductId == productId)
                {
                    return true;
                }
            }

            return false;
        }

        public void AddProduct(Product product)
        {
            Cart cart = GetCurrentCart();

            if (ProductExistsInCart(cart, product.Id))
            {
                CartItem cartItem = cart.CartItems.Single(ci => ci.ProductId == product.Id);

                cartItem.CountInCart++;
            }
            else
            {
                CartItem cartItem = new CartItem()
                {
                    ProductId = product.Id,
                    CountInCart = 1
                };

                cart.CartItems.Add(cartItem);
            }

            _context.SaveChanges();
        }

        public bool CanAddProductToCart(Product product)
        {
            //Fetch current pending cart
            var cart = GetCurrentCart();

            var cartItem = cart.CartItems.SingleOrDefault(ci => ci.ProductId == product.Id);

            if (cartItem == null)
            {
                //if cart item is null that means the count for that product in that cart is 0
                return product.InventoryCount > 0;
            }

            //product can only be added to a cart if it has enough inventory
            return cartItem.CountInCart < product.InventoryCount;
        }

        public double GetCartTotalAmt(Cart cart)
        {
            double total = 0;

            //Loop through all the items in cart
            foreach (CartItem cartItem in cart.CartItems)
            {
                //Fetch the product object based on productId to get the price information
                //of the product
                Product product = ProductRepository.GetProduct(cartItem.ProductId);

                total += cartItem.CountInCart * product.Price;
            }

            //Round the price off to 2 decimal places
            return Math.Round(total, 2);
        }

        public List<ProductViewModel> GetProductViewModelsInCart(Cart cart)
        {
            List<ProductViewModel> products = new List<ProductViewModel>();

            foreach (CartItem cartItem in cart.CartItems)
            {
                Product product = ProductRepository.GetProduct(cartItem.ProductId);

                products.Add(new ProductViewModel()
                {
                    Product = product.Title,
                    Price = product.Price,
                    Quantity = cartItem.CountInCart
                });
            }

            return products;
        }

        public void CompleteCart()
        {
            var cart = GetCurrentCart();

            foreach (var cartItem in cart.CartItems)
            {
                Product product = ProductRepository.GetProduct(cartItem.ProductId);

                product.ReduceInventory(cartItem.CountInCart);
            }

            cart.CompleteCart();

            _context.SaveChanges();
        }
    }
}