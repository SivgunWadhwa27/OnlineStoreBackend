using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using OnlineStoreBackend.Repositories;
using ShopifyOnlineStoreBackend.Models;
using ShopifyOnlineStoreBackend.ViewModels;

namespace OnlineStoreBackend.Controllers.api
{
    public class CartController : ApiController
    {
        private readonly CartRepository CartRepository;

        public CartController()
        {
            var context = new ApplicationDbContext();

            CartRepository = new CartRepository(context);
        }

        [HttpGet]
        public IHttpActionResult Info()
        {
            try
            {
                var cart = CartRepository.GetCurrentCart();

                List<ProductViewModel> products = CartRepository.GetProductViewModelsInCart(cart);

                double cartTotalAmt = CartRepository.GetCartTotalAmt(cart);

                return Json(new
                {
                    success = true,
                    products = products,
                    cartTotal = cartTotalAmt
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
        public IHttpActionResult Complete()
        {
            try
            {
                CartRepository.CompleteCart();

                return Json(new
                {
                    success = true,
                    message = "Cart successfully completed",
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
    }
}
