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
    public class LocationController : Controller
    {

        private readonly ILocation _locaiton;
        private readonly IOrders _orders;


        public LocationController(ILocation location, IOrders order)
        {
            _locaiton = location ?? throw new ArgumentNullException(nameof(location));
            _orders = order ?? throw new ArgumentNullException(nameof(location));
        }

        //// GET: /<controller>/
        //public IActionResult Index()
        //{
        //    return View();
        //}

        public ActionResult Index([FromQuery] string search = "")
        {
            var location = _locaiton.GetLocations(search);
            var viewModel = location.Select(a => new LocationViewModel
            {
                LocationId = a.LocationId,
                LocationName = a.LocationName
            });
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create([Bind("LocationName")] LocationViewModel locationViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(locationViewModel);
            }

            try
            {
                Location location = new Location
                {
                    LocationName = locationViewModel.LocationName
                };

                _locaiton.AddLocation(location);


                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(locationViewModel);
            }
        }

        public IActionResult Edit(int id)
        {
            Location location = _locaiton.GetById(id);
            var locationViewModel = new LocationViewModel
            {
                LocationId = location.LocationId,
                LocationName = location.LocationName
            };
            return View(locationViewModel);

        }

        [HttpPost]
        public IActionResult Edit([Bind("LocationName")] LocationViewModel locationViewModel, [FromRoute] int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Location location = _locaiton.GetById(id);
                    location.LocationName = locationViewModel.LocationName;
                    _locaiton.Update(location);

                    return RedirectToAction(nameof(Index));
                }
                return View(locationViewModel);
            }
            catch (Exception)
            {
                return View(locationViewModel);
            }
        }

        public IActionResult Delete(int id, [BindNever] IFormCollection collection)
        {
            try
            {
                _locaiton.DeleteLocation(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        //public IActionResult Details(int id)
        //{
        //    var location = _locaiton.GetById(id);
        //    var customerViewModel = new LocationViewModel
        //    {
        //        LocationId = location.LocationId,
        //        LocationName = location.LocationName
        //    };
        //    return View(customerViewModel);
        //}

        public IActionResult LocationOrderHistory()
        {
            ViewBag.locations = _locaiton.GetAll();

            return View(new LocationViewModel());
        }

        [HttpPost]
        public IActionResult LocationOrderHistory(int StoreId)
        {
            ViewBag.locations = _locaiton.GetAll();
            var location = _locaiton.GetById(StoreId);
            LocationViewModel viewModel = new LocationViewModel
            {
                LocationId = location.LocationId,
                LocationName = location.LocationName,
            };
            var orderHistory = _orders.OrderHistory(location);
            viewModel.Ohistory = orderHistory.Select(o => new OrderViewModel
            {
                OrderId = o.OrderId,
                Date = (DateTime)o.OrderDate,
                LocationName = o.Location.LocationName,
                CustomerName = o.Customer.FirstName,
                TotalCost = o.Price

            }).ToList();
            return View(viewModel);

        }

        public IActionResult Details(int OrderId)
        {
            Orders order = _orders.GetById(OrderId);
            OrderViewModel orderDetails = new OrderViewModel
            {
                OrderId = order.OrderId,
                Date = (DateTime)order.OrderDate,
                OrderLine = order.OrderLine,
                LocationName = order.Location.LocationName,
                CustomerName = order.Customer.FirstName,
                TotalCost = order.Price
            };
            return View(orderDetails);

        }
    }
}
