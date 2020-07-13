using System;
using System.Collections.Generic;
using CarStore.Library.Model;

namespace CarStore.Library.Interfaces
{
    public interface IOrders
    {
        IEnumerable<Orders> GetAll();

        void CreateOrder(Orders order);

        Orders GetById(int id);

        IEnumerable<Orders> OrderHistory(object historyType);

        public Dictionary<Product, int> OrderedProducts(int orderId);

    }
}
