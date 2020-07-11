using System;
using CarStore.Library.Model;

namespace CarStore.Library.Interfaces
{
    public interface IOrderLine
    {
        public OrderLine Add(OrderLine orderline, Product product, Orders order);
    }
}
