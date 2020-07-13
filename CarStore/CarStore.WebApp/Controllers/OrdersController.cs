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
    public class OrdersController : Controller
    {

        private readonly IOrders _orders;
        private readonly ILocation _location;
        private readonly IProduct _product;
        private readonly ICustomer _customer;
        private readonly PlaceOrder _placeOrder;

        public OrdersController(IOrders order, ILocation location, IProduct product, ICustomer customer)
        {
            _orders = order ?? throw new ArgumentNullException(nameof(order));
            _location = location ?? throw new ArgumentNullException(nameof(location));
            _product = product ?? throw new ArgumentNullException(nameof(product));
            _customer = customer ?? throw new ArgumentNullException(nameof(customer));
            _placeOrder = new PlaceOrder(_orders, _location);
        }

        // GET: /<controller>/
        //public IActionResult Index()
        //{
        //    return View();
        //}

        public IActionResult Index()
        {
            var locations = _location.GetAll().ToList();
            ViewBag.locations = locations;

            return View();
        }

        public IActionResult PlaceNewOrder(int locationId)
        {
            var location = _location.GetById(locationId);
            location.Stock = _location.GetProducts(location.LocationId);

            AddProducts addOrder = new AddProducts(location);
            string name = (string)TempData["Customer"];

            Customer customer;

            try
            {
                customer = _customer.GetByName(name);
            }
            catch
            {
                return RedirectToAction(nameof(Products));
            }

            foreach (var item in location.Stock.Keys)
            {
                if (item.ProductName == null)
                    return RedirectToAction(nameof(Products), new { location.LocationId });

                int quantity = (int)TempData[item.ProductName];
                if (quantity != 0)
                {
                    try
                    {
                        addOrder.AddToCart(item, quantity);
                    }
                    catch
                    {
                        return RedirectToAction(nameof(Products));
                    }
                }
            }

            if (addOrder.cart.Count == 0)
                return RedirectToAction(nameof(Products));

            try
            {
                _placeOrder.NewOrder(addOrder, customer);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Products));
            }
        }

        private IActionResult OrderHistory(string fName)
        {
            var history = _orders.OrderHistory(_customer.GetByName(fName));
            List<OrderViewModel> viewModels = new List<OrderViewModel>();
            if (fName != null)
            {
                foreach(var item in history)
                {
                    viewModels.Add(new OrderViewModel
                    {
                        Date = item.OrderDate,
                        OrderId = item.OrderId,
                        OrderLine = item.OrderLine,
                        LocationName = item.Location.LocationName,
                        TotalCost = item.Price
                    });
                }
                return View(viewModels);
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public IActionResult Products(int locationId)
        {
            var location = _location.GetById(locationId);
            location.Stock = _location.GetProducts(location.LocationId);
            return View(location);
        }

        [HttpPost]
        public IActionResult Products([Bind("ProductId", "Quantity")] OrderlineViewModel viewModel, int locationId)
        {
            var location = _location.GetById(locationId);

            location.Stock = _location.GetProducts(location.LocationId);

            if (ModelState.IsValid)
            {
                decimal price = 0;
                foreach (var item in location.Stock.Keys)
                {

                    if (viewModel == null) { }

                    int pCounter = 0;
                    if (item.ProductId == viewModel.ProductId)
                    {
                        pCounter += viewModel.Quantity;
                        if (location.Stock[item] < pCounter)
                            ModelState.AddModelError("", "not sufficiet stock to meet the request at this location");
                        else
                            TempData[item.ProductName] = pCounter;
                    }
                    TempData.Keep(item.ProductName);

                    price += item.Price * pCounter;
                }
                ViewData["Total"] = price;
                return View(location);
            }
            else
                return View(location);
        }

        
    }
}
