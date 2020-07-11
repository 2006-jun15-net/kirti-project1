using System;
using System.Collections.Generic;
using System.Linq;
using CarStore.DataAccess.Model;
using CarStore.Library.Interfaces;
using Microsoft.EntityFrameworkCore;
using Orders = CarStore.Library.Model.Orders;
using Cusotmer = CarStore.Library.Model.Customer;
using Location = CarStore.Library.Model.Location;


namespace CarStore.DataAccess.Repository
{
    public class OrdersRepo : IOrders
    {
        private readonly Project0Context _context;

        public OrdersRepo(Project0Context context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// make a order
        /// </summary>
        /// <param name="order"></param>
        public void CreateOrder(Orders order)
        {
            var addOrder = new Model.Orders
            {
                CustomerId = order.Customer.CustomerId,
                LocationId = order.Location.LocationId,
                OrderDate = order.OrderDate,
                Price = order.Price
            };

            _context.Orders.Add(addOrder);
            //_context.SaveChanges();

            foreach (var item in order.OrderLine.Keys)
            {
                addOrder.OrderLine.Add(new OrderLine
                {
                    ProductId = item.ProductId,
                    OrderId = addOrder.OrderId,
                    Quantity = order.OrderLine[item]
                });
            }
            _context.SaveChanges();
        }

        /// <summary>
        /// get all orders
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Orders> GetAll()
        {
            var order = _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.Location)
                .ToList();

            return order.Select(o => new Orders(
                o.OrderId,
                o.OrderDate,

                new Library.Model.Customer
                {
                    CustomerId = o.Customer.CustomerId,
                    FirstName = o.Customer.FirstName,
                    LastName = o.Customer.LastName
                },

                new Library.Model.Location
                {
                    LocationId = o.Location.LocationId,
                    LocationName = o.Location.LocationName
                },

                o.Price

            ));
        }

        /// <summary>
        /// get order by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Orders GetById(int id)
        {
            ProductRepo productRepo = new ProductRepo(_context);

            var entity = _context.Orders
                .Include(o => o.Location)
                .Include(o => o.Customer)
                .First(o => o.OrderId == id);
            Orders order = new Orders
            {
                OrderId = entity.OrderId,
                OrderDate = entity.OrderDate,
                Customer = new Library.Model.Customer
                {
                    CustomerId = entity.Customer.CustomerId,
                    FirstName = entity.Customer.FirstName,
                    LastName = entity.Customer.LastName,
                },
                Location = new Location
                {
                    LocationId = entity.Location.LocationId,
                    LocationName = entity.Location.LocationName
                },
                Price = entity.Price
            };
            order.OrderLine = productRepo.OrderedProducts(entity.OrderId);
            return order;
        }
    }
}
