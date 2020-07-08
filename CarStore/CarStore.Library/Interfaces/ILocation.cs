using System;
using System.Collections.Generic;
using CarStore.Library.Model;

namespace CarStore.Library.Interfaces
{
    public interface ILocation
    {
        IEnumerable<Location> GetAll();

        Location GetById(int locationId);

        void AddLocation(Location location);

        void Update(Location location);
    }
}