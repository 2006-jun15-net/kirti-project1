using System;
using System.Collections.Generic;
using System.Linq;
using CarStore.DataAccess.Model;
using CarStore.Library.Interfaces;
using Microsoft.EntityFrameworkCore;
using Product = CarStore.Library.Model.Product;

namespace CarStore.DataAccess.Repository
{
    public class ProductRepo : IProduct
    {
        private readonly Project0Context _context;

        public ProductRepo(Project0Context context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void AddProduct(Product product)
        {
            var addProduct = new Model.Product
            {
                ProductName = product.ProductName,
                Price = product.Price
            };

            _context.Product.Add(addProduct);
            _context.SaveChanges();
        }

        public Dictionary<Product, int> OrderedProducts(int orderId)
        {
            Dictionary<Product, int> productsInOrder = new Dictionary<Product, int>();

            var order = _context.Orders
                .Include(o => o.OrderLine)
                .First(o => o.OrderId == orderId);

            foreach (var item in order.OrderLine)
            {
                var purchasedProduct = _context.Product.Find(item.ProductId);

                Product product = new Product
                {
                    ProductId = purchasedProduct.ProductId,
                    ProductName = purchasedProduct.ProductName,
                    Price = purchasedProduct.Price
                };

                productsInOrder.Add(product, item.Quantity);
            }

            return productsInOrder;
        }
    }
}
