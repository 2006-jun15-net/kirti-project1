using System;
using System.Collections.Generic;
using System.Linq;
using CarStore.DataAccess.Model;
using CarStore.Library.Interfaces;
using CarStore.Library.Model;
using Stock = CarStore.Library.Model.Stock;

namespace CarStore.DataAccess.Repository
{
    public class StockRepo : IStock
    {
        private readonly Project0Context _context;

        public StockRepo(Project0Context context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// get all the inventory
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Stock> GetAll()
        {
            var stock = _context.Stock.ToList();

            return stock.Select(s => new Stock
            {
                StockId = s.StockId,
                Inventory = s.Inventory,
                LocationId = s.LocationId,
                ProductId = s.ProductId
            });
        }
    }
}
