using System;
using System.Collections.Generic;
using CarStore.Library.Interfaces;

namespace CarStore.Library.Model
{
    public class PlaceOrder
    {
        private readonly IOrders _oders;
        private readonly ILocation _location;

        public PlaceOrder (IOrders orders, ILocation location)
        {
            _oders = orders ?? throw new ArgumentNullException(nameof(location));
            _location = location ?? throw new ArgumentNullException(nameof(location));
        }

       

        public void NewOrder(AddProducts productInCart, Customer customer)
        {
            decimal orderTotal = 0;
            foreach (var item in productInCart.cart.Keys)
            {
                productInCart.Location.Stock[item] -= productInCart.cart[item];
                orderTotal += item.Price * productInCart.cart[item];
            }

            var newOrder = new Orders(productInCart, customer, orderTotal);

            _oders.CreateOrder(newOrder);
            _location.Update(productInCart.Location);
        }
    }
}
