using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Helpers;
using System.Web.Http;
using OnlineStoreBackend.Repositories;
using ShopifyOnlineStoreBackend.Models;

namespace OnlineStoreBackend.Controllers.api
{
    public class ProductController : ApiController
    {
        private readonly ProductRepository ProductRepository;
        private readonly CartRepository CartRepository;

        public ProductController()
        {
            var context = new ApplicationDbContext();
            ProductRepository = new ProductRepository(context);
            CartRepository = new CartRepository(context);
        }

        [HttpGet]
        public IHttpActionResult GetAllProducts(bool getProductsWithAvailableInventory = false)
        {
            try
            {
                //Get list of products, either available products or all products based on the parameter
                List<Product> products = getProductsWithAvailableInventory 
                    ? ProductRepository.GetProductsWithAvailableInventory()
                    : ProductRepository.GetAllProducts();

                return Json(new
                {
                    success = true,
                    products = products,
                });
            }
            catch (Exception e)
            {
                //Throw an exception and return the error message if anything goes wrong
                return Json(new
                {
                    success = false,
                    message = e.Message,
                });
            }
            
        }

        [HttpGet]
        public IHttpActionResult GetProduct(int productId)
        {
            try
            {
                Product product = ProductRepository.GetProduct(productId);

                if (product == null)
                {
                    return Json(new
                    {
                        success = false,
                        error = $"Product with id: {productId} not found.",
                    });
                }

                return Json(new
                {
                    success = true,
                    product = product,
                });
            }
            catch (Exception e)
            {
                return Json(new
                {
                    success = false,
                    message = e.Message,
                });
            }

        }

        [HttpPost]
        public IHttpActionResult Purchase(int productId)
        {
            try
            {
                Product product = ProductRepository.GetProduct(productId);

                if (product == null)
                {
                    return Json(new
                    {
                        success = false,
                        error = $"Product with id: {productId} not found.",
                    });
                }

                if (product.IsOutOfInventory())
                {
                    return Json(new
                    {
                        success = false,
                        error = $"Product with id: {productId} is out of inventory, hence cannot be purchased",
                    });
                }

                //Check if product has enough inventory to be able to add to the cart
                if (!CartRepository.CanAddProductToCart(product))
                {
                    return Json(new
                    {
                        success = false,
                        error = $"Product with id: {productId} does not have enough inventory to be added to cart",
                    });
                }

                CartRepository.AddProduct(product);

                return Json(new
                {
                    success = false,
                    error = $"Product with id: {productId} successfully purchased",
                });

            }
            catch (Exception e)
            {
                return Json(new
                {
                    success = false,
                    error = e.Message,
                });
            }
        }
    }
}
