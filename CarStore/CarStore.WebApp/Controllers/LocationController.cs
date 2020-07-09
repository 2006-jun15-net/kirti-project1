using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarStore.Library.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CarStore.WebApp.Controllers
{
    public class LocationController : Controller
    {

        private readonly ILocation _locaiton;

        public LocationController(ILocation location)
        {
            _locaiton = location ?? throw new ArgumentNullException(nameof(location));
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
    }
}
