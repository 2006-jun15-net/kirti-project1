using System;
using System.Collections.Generic;
using System.Linq;
using CarStore.DataAccess.Model;
using CarStore.Library.Interfaces;
using Microsoft.EntityFrameworkCore;
using Orders = CarStore.Library.Model.Orders;

namespace CarStore.DataAccess.Repository
{
    public class OrdersRepo : IOrders
    {
        private readonly Project0Context _context;

        public OrdersRepo(Project0Context context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

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
                    Quantity = order.OrderLine[item]
                });
            }
            _context.SaveChanges();
        }

        public IEnumerable<Orders> GetAll()
        {
            var order = _context.Orders
                .Include(o => o.Location)
                .Include(o => o.Customer)
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

        public IEnumerable<Orders> OrderHistory(object historyType)
        {
            ProductRepo productRepo = new ProductRepo(_context);
            if (historyType is Location)
            {
                Location location = (Location)historyType;

                var orderHistory = _context.Orders
                    .Include(o => o.Location)
                    .Include(o => o.Customer)
                    .Where(o => o.CustomerId == location.LocationId);

                var orders = orderHistory.Select(o => new Orders(
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

                var orderList = orders.ToList();
                foreach (var item in orderList)
                {
                    item.OrderLine = productRepo.OrderedProducts(item.OrderId);
                }
            }
            else if (historyType is Customer)
            {
                Customer customer = (Customer)historyType;

                var customerHistory = _context.Orders
                    .Include(o => o.Location)
                    .Include(o => o.Customer)
                    .Where(o => o.CustomerId == customer.CustomerId);

                var customerOrders = customerHistory.Select(o => new Orders(
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

                var orderList = customerOrders.ToList();
                foreach (var item in orderList)
                {
                    item.OrderLine = productRepo.OrderedProducts(item.OrderId);
                }
                return customerOrders;
            }
            return null;
        }
    }
}
