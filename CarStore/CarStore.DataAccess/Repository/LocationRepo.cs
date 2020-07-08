using System;
using System.Collections.Generic;
using System.Linq;
using CarStore.DataAccess.Model;
using CarStore.Library.Interfaces;
using Microsoft.EntityFrameworkCore;
using Location = CarStore.Library.Model.Location;

namespace CarStore.DataAccess.Repository
{
    public class LocationRepo : ILocation
    {

        private readonly Project0Context _context;

        public LocationRepo(Project0Context context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void AddLocation(Location location)
        {
            var addLocation = new Model.Location
            {
                LocationName = location.LocationName
            };

            _context.Location.Add(addLocation);
            _context.SaveChanges();
        }

        public IEnumerable<Location> GetAll()
        {
            var location = _context.Location.ToList();

            return location.Select(l => new Location
            {
                LocationId = l.LocationId,
                LocationName = l.LocationName
            });
        }

        public Location GetById(int locationId)
        {
            var location = _context.Location.Find(locationId);

            return new Location
            {
                LocationId = location.LocationId,
                LocationName = location.LocationName
            };
        }

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
    }
}
