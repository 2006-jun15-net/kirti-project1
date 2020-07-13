
using System;
using System.Collections.Generic;

namespace CarStore.Library.Model
{
    /// <summary>
    /// orders object: id, location, customer, price, date of order
    /// </summary>
    public class Orders
    {
        //private fields
        private Location _locationId;
        private Customer _customerId;
        private decimal _price;

        public Orders()
        {
        }

        public Orders(int orderId, DateTime? orderDate, Customer customer, Location location, decimal totalPrice)
        {
            OrderId = orderId;
            OrderDate = (DateTime)orderDate;
            Location = location;
            Customer = customer;
            Price = totalPrice;
        }

        public Orders(Location location, DateTime orderDate, Dictionary<Product, int> orderedProducts, Customer customer, decimal totalPrice)
        {
            Location = location;
            OrderDate = orderDate;
            OrderLine = orderedProducts;
            Customer = customer;
            Price = totalPrice;
        }

        public Orders (AddProducts orderInCart, Customer customer, decimal totalPrice) : this(orderInCart.Location, DateTime.Now, orderInCart.cart, customer, totalPrice)
        {
        }

        /// <summary>
        /// order id
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// location of where the order must be placed
        /// </summary>
        public Location Location
        {
            get => _locationId;
            set
            {
                if (value == null)
                    throw new ArgumentException("Location not selected, cannot place order without selecting location", nameof(value));

                _locationId = value;
            }
        }

        /// <summary>
        /// customers id to associate the order with
        /// </summary>
        public Customer Customer
        {
            get => _customerId;
            set
            {
                if (value == null)
                    throw new ArgumentException("Customer not selected, cannot place order without selecting location", nameof(value));

                _customerId = value;
            }
        }

        /// <summary>
        /// cost of the order
        /// </summary>
        public decimal Price
        {
            get;
            set;
        }

        /// <summary>
        /// date and time it was ordered at
        /// </summary>
        public DateTime OrderDate { get; set; }

        public Dictionary<Product, int> OrderLine { get; set; } = new Dictionary<Product, int>();

    }
}