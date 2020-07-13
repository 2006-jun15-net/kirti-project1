using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarStore.Library.Interfaces;
using CarStore.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CarStore.WebApp.Controllers
{
    public class ProductController : Controller
    {

        private readonly IProduct _product;


        public ProductController(IProduct product)
        {
            _product = product ?? throw new ArgumentNullException(nameof(product));
        }


        // GET: /<controller>/
        //public IActionResult Index()
        //{
        //    return View();
        //}

        public ActionResult Index([FromQuery] string search = "")
        {
            var product = _product.GetProducts(search);
            var viewModel = product.Select(a => new ProductViewModel
            {
                ProductId = a.ProductId,
                ProductName = a.ProductName,
                Price = a.Price
            });
            return View(viewModel);
        }
    }
}
