using System;
using System.Collections.Generic;
using System.Linq;
using CarStore.DataAccess.Model;
using CarStore.Library.Interfaces;
using Microsoft.EntityFrameworkCore;
using Location = CarStore.Library.Model.Location;
using Product = CarStore.Library.Model.Product;

namespace CarStore.DataAccess.Repository
{
    public class LocationRepo : ILocation
    {

        private readonly Project0Context _context;

        public LocationRepo(Project0Context context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// add new location
        /// </summary>
        /// <param name="location"></param>
        public void AddLocation(Location location)
        {
            var addLocation = new Model.Location
            {
                LocationName = location.LocationName
            };

            _context.Location.Add(addLocation);
            _context.SaveChanges();
        }

        /// <summary>
        /// get all locations
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Location> GetAll()
        {
            var location = _context.Location.ToList();

            return location.Select(l => new Location
            {
                LocationId = l.LocationId,
                LocationName = l.LocationName
            });
        }

        /// <summary>
        /// get location by id
        /// </summary>
        /// <param name="locationId"></param>
        /// <returns></returns>
        public Location GetById(int locationId)
        {
            var location = _context.Location.Find(locationId);

            return new Location
            {
                LocationId = location.LocationId,
                LocationName = location.LocationName
            };
        }

        /// <summary>
        /// update location
        /// </summary>
        /// <param name="location"></param>
        public void Update(Location location)
        {
            var updateLocation = _context.Location
                .Include(l => l.Stock)
                .First(l => l.LocationId == location.LocationId);

            foreach (var item in location.Stock.Keys)
                updateLocation.Stock.First(l => l.ProductId == item.ProductId)
                    .Inventory = location.Stock[item];

            _context.SaveChanges();
        }

        /// <summary>
        /// get locations
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public IEnumerable<Location> GetLocations(string search = null)
        {
            IQueryable<Model.Location> items = _context.Location;
            if (search != null)
            {
                items = items.Where(r => r.LocationName.Contains(search));
            }
            return items.Select(e => new Location(e.LocationId, e.LocationName));
        }

        /// <summary>
        /// delete locations
        /// </summary>
        /// <param name="locationId"></param>
        public void DeleteLocation(int locationId)
        {
            Model.Location location = _context.Location.Find(locationId);
            _context.Remove(location);
            _context.SaveChanges();
        }

        /// <summary>
        /// get each product's inventory at that location
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Dictionary<Product, int> GetProducts(int id)
        {
            Dictionary<Product, int> locationInventory = new Dictionary<Product, int>();

            var entity = _context.Location
                .Include(s => s.Stock)
                .First(l => l.LocationId == id);

            foreach (var item in entity.Stock)
            {
                var product = _context.Product.Find(item.ProductId);

                Product countProducts = new Product
                {
                    ProductId = product.ProductId,
                    ProductName = product.ProductName,
                    Price = product.Price
                };

                locationInventory.Add(countProducts, item.Inventory);
            }
            return locationInventory;
        }
    }
}
