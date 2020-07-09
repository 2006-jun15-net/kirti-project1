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
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index([Bind("FirstName, LastName")] CustomerViewModel customerViewModel)
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

        public IActionResult Search(string search = null)
        {
            if (search != null)
            {
                if (_customer.GetAll().Any(c => c.FirstName.Equals(search)))
                {
                    return RedirectToAction(nameof(Details), new { fName = search });
                }
                return View();
            }
            return View();
        }

        public IActionResult Edit([FromRoute] string name)
        {
            Customer customer = _customer.GetByName(name);
            var viewModel = new CustomerViewModel
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName,
            };
            return View(viewModel);

        }

        [HttpPost]
        public IActionResult Edit([Bind("FirstName, LastName")] CustomerViewModel customerViewModel, [FromRoute] string name)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Customer customer = _customer.GetByName(name);
                    customer.FirstName = customerViewModel.FirstName;
                    customer.LastName = customerViewModel.LastName;
                    _customer.Update(customer);

                    return RedirectToAction(nameof(Details), new { fName = customer.FirstName });
                }
                return View(customerViewModel);
            }
            catch (Exception)
            {
                return View(customerViewModel);
            }

        }
    }
}