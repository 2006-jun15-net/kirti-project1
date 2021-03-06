﻿using System;
using System.Collections.Generic;
using CarStore.Library.Model;

namespace CarStore.Library.Interfaces
{
    public interface ICustomer
    {
        IEnumerable<Customer> GetAll();

        IEnumerable<Customer> GetCustomers(string search = null);

        Customer GetById(int id);

        Customer GetByName(string fName);

        void AddCustomer(Customer customer);

        void Update(Customer customer);

        public void DeleteCustomer(int customerId);
    }
}
