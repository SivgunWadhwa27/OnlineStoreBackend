using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopifyOnlineStoreBackend.ViewModels
{
    //The purpose of this class is to encapsulate Product information to only show limited information
    public class ProductViewModel
    {
        public string Product { get; set; }

        public double Price { get; set; }

        public int  Quantity { get; set; }
    }
}