using System;
using System.Collections.Generic;
using CarStore.Library.Interfaces;

namespace CarStore.Library.Model
{
    public class PlaceOrder
    {
        private readonly IOrders _oders;
        private readonly ILocation _location;
        public Dictionary<Product, int> orderedProducts { get; } = new Dictionary<Product, int>();

        public Location Location { get; }

        public PlaceOrder (IOrders orders, ILocation location)
        {
            _oders = orders;
            _location = location;
        }

        public PlaceOrder (Location location)
        {
            Location = location ?? throw new ArgumentNullException(nameof(location));
        }

        public void AddProductsToOrder(Product product, int quantity)
        {
            if (quantity > Location.Stock[product])
                throw new ArgumentException("Product quantity is more than available stock");

            orderedProducts.Add(product, quantity);
        }

        public void NewOrder(Customer customer)
        {
            decimal totalCost = 0;
            foreach (var item in orderedProducts.Keys)
            {
                Location.Stock[item] -= orderedProducts[item];
                totalCost += item.Price * orderedProducts[item];
            }

            var newOrder = new Orders(Location, orderedProducts, customer, totalCost);

            _oders.CreateOrder(newOrder);
            _location.Update(Location);
        }
    }
}
