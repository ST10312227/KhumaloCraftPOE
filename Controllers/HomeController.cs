
using KhumaloCraftPOE.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;

namespace KhumaloCraftPOE.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly userTable usrTable = new userTable();
        private readonly productTable prodTable = new productTable();
        private readonly transactionTable tratbl = new transactionTable();
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HomeController(ILogger<HomeController> logger, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Index()
        {
            // Retrieve userID from claims
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            int userID = userIdClaim != null ? int.Parse(userIdClaim.Value) : 0;

            // Pass userID and other data to the view
            ViewData["userID"] = userID;
            var products = prodTable.GetProducts();
            var users = usrTable.GetUsers();
            var userTransactions = tratbl.GetUserTransactions(userID);

            ViewBag.Users = users;
            ViewBag.Products = products;
            ViewBag.UserTransactions = userTransactions;

            // Retrieve userID from session (if necessary)
            int? sessionUserID = _httpContextAccessor.HttpContext.Session.GetInt32("UserID");
            if (sessionUserID != null)
            {
                ViewBag.SessionUserID = sessionUserID;
            }

            if (TempData.ContainsKey("ErrorMessage"))
            {
                ViewBag.ErrorMessage = TempData["ErrorMessage"];
            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult About()
        {
            // Retrieve user data from the database
            var users = usrTable.GetUsers();
            // Pass the user data to the view
            return View(users);
        }

        public IActionResult MyWork()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
