using System;
using System.Collections.Generic;
using CarStore.Library.Model;

namespace CarStore.WebApp.Models
{
    public class PlaceOrderViewModel
    {
        public Dictionary<Product, int> orderedProducts { get; set; }

        public int StoreId { get; set; }

        public decimal _price;
        public decimal Price
        {
            get
            {
                foreach (var item in orderedProducts.Keys)
                {
                    _price += item.Price * orderedProducts[item];
                }
                return _price;
            }
        }

    }
}
