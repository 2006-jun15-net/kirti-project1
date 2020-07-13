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

        /// <summary>
        /// Add new cusotmer
        /// </summary>
        /// <param name="customer"></param>
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

        /// <summary>
        /// Get all the customers
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// get custoemrs 
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public IEnumerable<Customer> GetCustomers(string search = null)
        {
            IQueryable<Model.Customer> items = _context.Customer;
            if (search != null)
            {
                items = items.Where(r => r.FirstName.Contains(search));
            }
            return items.Select(e => new Customer(e.CustomerId, e.FirstName, e.LastName));
        }

        /// <summary>
        /// get cusotmer by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Customer GetById(int id)
        {
            Model.Customer customer = _context.Customer.FirstOrDefault(c => c.CustomerId == id);

            return new Customer
            {
                CustomerId = customer.CustomerId,
                FirstName = customer.FirstName,
                LastName = customer.LastName
            };
        }

        /// <summary>
        /// update cusotmer
        /// </summary>
        /// <param name="customer"></param>
        public void Update(Customer customer)
        {
            var currentCustomer = _context.Customer.Find(customer.CustomerId);

            var updateCustomer = new Model.Customer
            {
                CustomerId = currentCustomer.CustomerId,
                FirstName = customer.FirstName,
                LastName = customer.LastName
            };

            _context.Entry(currentCustomer).CurrentValues.SetValues(updateCustomer);
            _context.SaveChanges();
        }

        /// <summary>
        /// delete cusotmer 
        /// </summary>
        /// <param name="customerId"></param>
        public void DeleteCustomer(int customerId)
        {
            Model.Customer customer = _context.Customer.Find(customerId);
            _context.Remove(customer);
            _context.SaveChanges();
        }

        public Customer GetByName(string fName)
        {
            Model.Customer customer = _context.Customer.First(c => c.FirstName.Equals(fName));
            return new Customer
            {
                CustomerId = customer.CustomerId,
                FirstName = customer.FirstName,
                LastName = customer.LastName

            };
        }
    }
}
