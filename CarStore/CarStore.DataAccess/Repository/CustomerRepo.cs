using System;
using System.Collections.Generic;
using System.Linq;
using CarStore.DataAccess.Model;
using CarStore.Library.Interfaces;
using Customer = CarStore.Library.Model.Customer;

namespace CarStore.DataAccess.Repository
{
    /// <summary>
    /// customer repository
    /// </summary>
    public class CustomerRepo : ICustomer
    {
        private readonly Project0Context _context;

        public CustomerRepo(Project0Context context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void AddCustomer(Customer customer)
        {
            var addCustomer = new Model.Customer
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName
            };

            _context.Customer.Add(addCustomer);
            _context.SaveChanges();
        }

        //might get rid of it 
        public void Delete(Customer customer)
        {
            var deleteCustomer = _context.Customer.First(c => c.FirstName.Equals(customer.FirstName));

            _context.Customer.Remove(deleteCustomer);
            _context.SaveChanges();
        }

        public IEnumerable<Customer> GetAll()
        {
            var customer = _context.Customer.ToList();

            return customer.Select(c => new Customer
            {
                CustomerId = c.CustomerId,
                FirstName = c.FirstName,
                LastName = c.LastName
            });
        }

        public Customer GetByName(string firstName)
        {
            Model.Customer customer = _context.Customer.First(c => c.FirstName.Equals(firstName));

            return new Customer
            {
                CustomerId = customer.CustomerId,
                FirstName = customer.FirstName,
                LastName = customer.LastName
            };
        }

        //might get rid of it 
        public void Update(Customer customer)
        {
            throw new NotImplementedException();
        }
    }
}
