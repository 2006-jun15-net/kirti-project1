using System;
using CarStore.Library.Model;
using CarStore.Library.Interfaces;
using CarStore.DataAccess.Model;
using Product = CarStore.Library.Model.Product;
using Orders = CarStore.Library.Model.Orders;
using OrderLine = CarStore.Library.Model.OrderLine;

namespace CarStore.DataAccess.Repository
{
    public class OrderLineRepo : IOrderLine
    {
        private readonly Project0Context _context;

        public OrderLineRepo(Project0Context context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// add new order to order line
        /// </summary>
        /// <param name="orderline"></param>
        /// <param name="product"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public OrderLine Add(OrderLine orderline, Product product, Orders order)
        {
            var addOrder = new OrderLine
            {
                OrderId = order.OrderId,
                ProductId = product.ProductId,
                Quantity = orderline.Quantity
            };

            return addOrder;
        }

    }
}
