using System;
using System.Collections.Generic;

namespace CarStore.Library.Model
{
    public class AddProducts
    {
        public AddProducts()
        {
        }

        public Dictionary<Product, int> cart { get; } = new Dictionary<Product, int>();
        public Location Location { get; }

        public AddProducts(Location location)
        {
            Location = location ?? throw new ArgumentNullException(nameof(location));
        }

        public void AddToCart(Product p, int quantity)
        {
            if (quantity > Location.Stock[p])
            {
                throw new ArgumentException("more quanttiy of this product than it is in stock.");
            }

            cart.Add(p, quantity);
        }
    }
}
