using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarStore.Library.Interfaces;
using CarStore.Library.Model;
using CarStore.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CarStore.WebApp.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomer _customer;

        public CustomerController(ICustomer customer)
        {
            _customer = customer ?? throw new ArgumentNullException(nameof(customer));
        }

        // GET: /<controller>/
        [HttpGet]
        public IActionResult AddCustomer()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RegisterCustomer([Bind("FirstName, LastName")] CustomerViewModel customerViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(customerViewModel);
            }

            try
            {
                Customer customer = new Customer
                {
                    FirstName = customerViewModel.FirstName,
                    LastName = customerViewModel.LastName
                };

                _customer.AddCustomer(customer);
                return RedirectToAction(nameof(Details), new { FirstName = customer.FirstName });
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Error, try again.");
                return View();
            }
        }

        public IActionResult Details(string fName)
        {
            Customer customer = _customer.GetByName(fName);
            var customerViewModel = new CustomerViewModel
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName,
            };
            return View(customerViewModel);
        }
    }
}
