using System;
using System.Collections.Generic;
using CarStore.Library.Model;

namespace CarStore.Library.Interfaces
{
    public interface IProduct
    {
        //public Dictionary<Product, int> OrderedProducts(int orderId);

        void AddProduct(Product product);

        public IEnumerable<Product> GetProducts(string search = null);
    }
}
