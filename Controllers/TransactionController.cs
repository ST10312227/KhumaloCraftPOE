using KhumaloCraftPOE.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Transactions;

namespace KhumaloCraftPOE.Controllers
{
    public class TransactionController : Controller
    {
        private readonly string con_string = "Server=tcp:cldvpoe2.database.windows.net,1433;Initial Catalog=st10312227;Persist Security Info=False;User ID=kresen;Password=Naickerk94!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30";
        private readonly transactionTable tratbl = new transactionTable();
        private readonly productTable prodtbl = new productTable();
        private readonly userTable usrtbl = new userTable();

        [HttpPost]
        public IActionResult AddToCart(int productId, int userId)
        {
            // Create a new transaction using the submitted product ID and quantity
            var transaction = new transactionTable
            {
                UserID = userId, // Assign the userID to the transaction
                Name = GetUserName(userId),
                Surname = GetUserSurname(userId),
                Email = GetUserEmail(userId),
                ProductID = productId, // Assign the productID to the transaction
                Price = GetProductPrice(productId), // Implement this method to fetch product price based on productId
                Date = DateTime.Today,


            };


            // Insert the new transaction into the database
            int rowsAffected = tratbl.insert_transaction(transaction);

            if (rowsAffected > 0)
            {
                UpdateProductAvailability(productId);
                // If insertion is successful, redirect to the home page or any other appropriate page
                return RedirectToAction("Index", "Home");
            }
            else
            {
                // If insertion fails, handle the error accordingly
                // You can choose to display an error message or redirect to an error page
                return RedirectToAction("Error");
            }

        }

        // Implement this method to fetch product price based on productId
        private decimal GetProductPrice(int productID)
        {
            // Retrieve all products from the database
            var products = prodtbl.GetProducts();

            // Find the product with the specified ID
            var product = products.FirstOrDefault(p => p.ProductID == productID);

            // Return the price of the product if found, otherwise return 0
            return product != null ? decimal.Parse(product.Price) : 0;
        }

        private string GetUserName(int userId)
        {
            var users = usrtbl.GetUsers();

            var user = users.FirstOrDefault(u => u.UserID == userId);

            return user != null ? user.Name : "";

        }

        private string GetUserSurname(int userId)
        {
            var users = usrtbl.GetUsers();

            var user = users.FirstOrDefault(u => u.UserID == userId);

            return user != null ? user.Surname : "";

        }

        private string GetUserEmail(int userId)
        {
            var users = usrtbl.GetUsers();

            var user = users.FirstOrDefault(u => u.UserID == userId);

            return user != null ? user.Email : "";

        }

        private void UpdateProductAvailability(int productId)
        {
            using (SqlConnection con = new SqlConnection(con_string))
            {
                string sql = "UPDATE productTable SET productAvailability = 'Not Available' WHERE ProductID = @ProductId";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@ProductId", productId);
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    // Handle exception
                }
            }
        }


    }
}
