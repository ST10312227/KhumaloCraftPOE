using KhumaloCraftPOE.Models;
using Microsoft.AspNetCore.Mvc;

namespace KhumaloCraftPOE.Controllers
{
    public class ProductController : Controller
    {
        private readonly productTable prodtbl = new productTable();
        private readonly transactionTable tratbl = new transactionTable();

        [HttpPost]
        public ActionResult MyWork(productTable product)
        {
            // Insert the product into the database
            var result = prodtbl.insert_product(product);

            // Redirect to the Privacy page after insertion
            return RedirectToAction("MyWork", "Product");
        }

        [HttpGet]
        public ActionResult MyWork()
        {
            // Retrieve products and transactions from the database
            var products = prodtbl.GetProducts();
            var transactions = tratbl.GetTransactions();

            // Pass products and transactions to the view
            ViewData["Products"] = products;
            ViewData["Transactions"] = transactions;

            return View();
        }
    }
}
