using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopifyOnlineStoreBackend.Models
{
    public class Cart
    {
        public int Id { get; set; }

        public bool Complete { get; set; }

        public ICollection<CartItem> CartItems { get; set; }

        public Cart()
        {
            CartItems = new List<CartItem>();
        }


        public void CompleteCart()
        {
            Complete = true;
        }
    }
}