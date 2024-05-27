using Microsoft.AspNetCore.Mvc;
using KhumaloCraftPOE.Models;


namespace KhumaloCraftPOE.Controllers
{
    public class UserController : Controller
    {
        private readonly userTable usrtbl = new userTable();

        [HttpGet]
        public IActionResult About()
        {
            // Retrieve user data from the database
            var users = usrtbl.GetUsers();

            // Pass the user data to the view
            return View(users);
        }

        [HttpPost]
        public IActionResult About(userTable users)
        {
            if (ModelState.IsValid)
            {
                // Insert the new user into the database
                int rowsAffected = usrtbl.insert_User(users);
                if (rowsAffected > 0)
                {
                    // If insertion is successful, redirect to the About page to display the updated list
                    return RedirectToAction("About");
                }
                else
                {
                    // If insertion fails, handle the error accordingly
                    // You can choose to display an error message or redirect to an error page
                    return RedirectToAction("Error");
                }
            }
            else
            {
                // If model validation fails, return the view with validation errors
                return View(users);
            }
        }
    }
}