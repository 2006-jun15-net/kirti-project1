using System;
using System.Collections.Generic;
using CarStore.Library.Model;

namespace CarStore.Library.Interfaces
{
    public interface ICustomer
    {
        IEnumerable<Customer> GetAll();

        IEnumerable<Customer> GetCustomers(string search = null);

        Customer GetById(int id);

        void AddCustomer(Customer customer);

        void Delete(Customer customer);

        void Update(Customer customer);
    }
}
