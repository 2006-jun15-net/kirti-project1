using System;
using System.Collections.Generic;
using CarStore.Library.Model;

namespace CarStore.Library.Interfaces
{
    public interface IOrders
    {
        IEnumerable<Orders> GetAll();

        void CreateOrder(Orders order);

        public Orders GetById(int id);
    }
}
