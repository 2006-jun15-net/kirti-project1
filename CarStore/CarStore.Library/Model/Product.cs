using System;

namespace CarStore.Library.Model
{
    /// <summary>
    /// product object
    /// </summary>
    public class Product
    {
        // private fields for product name to ensure the name is valid
        private string _name;
        private decimal _price;

        public Product()
        {
        }

        public Product(int id, string name, decimal price)
        {
            ProductId = id;
            ProductName = name;
            Price = price;
        }

        /// <summary>
        /// product id
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// product name, cannot be empty
        /// </summary>
        public string ProductName
        {
            get => _name;
            set
            {
                if (value.Length == 0)
                    throw new ArgumentException("First-name cannot be empty", nameof(value));

                _name = value;
            }
        }

        /// <summary>
        /// price of the product
        /// </summary>
        public decimal Price
        {
            get;
            set;
        }

    }
}