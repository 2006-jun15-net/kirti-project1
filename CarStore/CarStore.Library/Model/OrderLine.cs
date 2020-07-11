using System;
namespace CarStore.Library.Model
{
    public class OrderLine
    {
        private int _quantity;

        public OrderLine()
        {
        }

        public int Quantity
        {
            get => _quantity;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Invalid product quantity.", nameof(value));
                }
                _quantity = value;
            }
        }

        public int OrderId { get; set; }

        public int ProductId { get; set; }
    }
}
