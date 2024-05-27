using Microsoft.AspNetCore.Mvc;
using KhumaloCraftPOE.Models;

namespace KhumaloCraftPOE.Controllers
{
    public class ProductDisplayController : Controller
    {
        [HttpGet]
        public IActionResult MyWork()
        {
            var products = ProductDisplayModel.SelectProducts();
            return View(products);
        }
    }
}