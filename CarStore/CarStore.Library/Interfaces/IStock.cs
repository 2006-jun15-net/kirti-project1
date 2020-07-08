using System;
using System.Collections.Generic;
using CarStore.Library.Model;

namespace CarStore.Library.Interfaces
{
    public interface IStock
    {
        IEnumerable<Stock> GetAll();
    }
}
