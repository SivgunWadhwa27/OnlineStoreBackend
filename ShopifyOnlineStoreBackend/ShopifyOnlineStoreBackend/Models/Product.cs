using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopifyOnlineStoreBackend.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public int InventoryCount { get; set; }

        public bool IsOutOfInventory()
        {
            return InventoryCount == 0;
        }

        public void Purchase()
        {
            InventoryCount--;
        }

        public void ReduceInventory(int amountPurchased)
        {
            InventoryCount -= amountPurchased;
        }
    }
}