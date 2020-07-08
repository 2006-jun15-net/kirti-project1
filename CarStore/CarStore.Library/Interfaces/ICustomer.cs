using System;
using System.Collections.Generic;
using CarStore.Library.Model;

namespace CarStore.Library.Interfaces
{
    public interface ICustomer
    {
        IEnumerable<Customer> GetAll();

        Customer GetByName(string firstName);

        void AddCustomer(Customer customer);

        void Delete(Customer customer);

        void Update(Customer customer);
    }
}
