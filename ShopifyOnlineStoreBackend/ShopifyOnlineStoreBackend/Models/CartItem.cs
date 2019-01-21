using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopifyOnlineStoreBackend.Models
{
    public class CartItem
    {
        public int Id { get; set; }

        public int  CountInCart { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }

    }
}