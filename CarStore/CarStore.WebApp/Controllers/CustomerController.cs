using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarStore.Library.Interfaces;
using CarStore.Library.Model;
using CarStore.WebApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CarStore.WebApp.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomer _customer;
        private readonly IOrders _orders;


        public CustomerController(ICustomer customer, IOrders order)
        {
            _customer = customer ?? throw new ArgumentNullException(nameof(customer));
            _orders = order ?? throw new ArgumentNullException(nameof(order));

        }

        ////GET: /<controller>/
        //[HttpGet]
        //public IActionResult Index()
        //{
        //    return View();
        //}

        public ActionResult Index([FromQuery] string search = "")
        {
            var customers = _customer.GetCustomers(search);
            var viewModel = customers.Select(a => new CustomerViewModel
            {
                CustomerId = a.CustomerId,
                FirstName = a.FirstName,
                LastName = a.LastName
            });
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Create ()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create([Bind("FirstName, LastName")] CustomerViewModel customerViewModel)
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
                TempData["Customer"] = customer.FirstName;

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(customerViewModel);
            }
        }

        public IActionResult Details(int id)
        {
            var customer = _customer.GetById(id);
            var customerViewModel = new CustomerViewModel
            {
                CustomerId = customer.CustomerId,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Orders = customer.Ohistory.Select(c => new OrderViewModel
                {
                    OrderId = c.OrderId,
                    Date = c.OrderDate,
                    TotalCost = c.Price,
                    OrderLine = (Dictionary<Product, int>)c.OrderLine.Select(s => new OrderlineViewModel
                    {
                        ProductId = s.Key.ProductId,
                        Quantity = s.Value
                    })
                }).ToList()

            };
            TempData["Customer"] = customer.FirstName;

            return View(customerViewModel);
        }

        public IActionResult Edit(int id)
        {
            Customer customer = _customer.GetById(id);
            var customerViewModel = new CustomerViewModel
            {
                CustomerId = customer.CustomerId,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
            };
            return View(customerViewModel);

        }

        [HttpPost]
        public IActionResult Edit([Bind("FirstName, LastName")] CustomerViewModel customerViewModel, [FromRoute] int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Customer customer = _customer.GetById(id);
                    customer.FirstName = customerViewModel.FirstName;
                    customer.LastName = customerViewModel.LastName;
                    _customer.Update(customer);

                    return RedirectToAction(nameof(Index));
                }
                return View(customerViewModel);
            }
            catch (Exception)
            {
                return View(customerViewModel);
            }
        }

        public IActionResult Delete(int id, [BindNever] IFormCollection collection)
        {
            try
            {
                _customer.DeleteCustomer(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}