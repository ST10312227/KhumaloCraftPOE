using System;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Transactions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace KhumaloCraftPOE.Models
{
    public class transactionTable
    {
        public static string con_string = "Server=tcp:cldvpoe2.database.windows.net,1433;Initial Catalog=st10312227;Persist Security Info=False;User ID=kresen;Password=Naickerk94!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30";

        public static SqlConnection con = new SqlConnection(con_string);


        public int TransactionID { get; set; }
        public int UserID { get; set; } // Foreign key referencing userTable
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public int ProductID { get; set; }
        public decimal Price { get; set; }
        public DateTime Date { get; set; }

        public int insert_transaction(transactionTable transaction)
        {
            try
            {
                string sql = "INSERT INTO transactionTable (userID, name, surname, email, productID, price, date) VALUES (@UserID, @Name, @Surname, @Email, @ProductID, @Price, @Date)";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@UserID", transaction.UserID);
                cmd.Parameters.AddWithValue("@Name", transaction.Name);
                cmd.Parameters.AddWithValue("@Surname", transaction.Surname);
                cmd.Parameters.AddWithValue("@Email", transaction.Email);
                cmd.Parameters.AddWithValue("@ProductID", transaction.ProductID);
                cmd.Parameters.AddWithValue("@Price", transaction.Price);
                cmd.Parameters.AddWithValue("@Date", transaction.Date);
                con.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                con.Close();
                return rowsAffected;
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                // For now, rethrow the exception
                throw ex;
            }
        }
        public List<transactionTable> GetTransactions()
        {
            List<transactionTable> transactions = new List<transactionTable>();

            try
            {
                using (SqlConnection con = new SqlConnection("Server=tcp:cldvpoe2.database.windows.net,1433;Initial Catalog=st10312227;Persist Security Info=False;User ID=kresen;Password=Naickerk94!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30"))
                {
                    string sql = "SELECT * FROM transactionTable";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        con.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                transactionTable transaction = new transactionTable
                                {
                                    TransactionID = Convert.ToInt32(reader["TransactionID"]),
                                    UserID = Convert.ToInt32(reader["UserID"]),
                                    Name = Convert.ToString(reader["Name"]),
                                    Surname = Convert.ToString(reader["Surname"]),
                                    Email = Convert.ToString(reader["Email"]),
                                    ProductID = Convert.ToInt32(reader["ProductID"]),
                                    Price = Convert.ToDecimal(reader["Price"]),
                                    Date = Convert.ToDateTime(reader["Date"])
                                };
                                transactions.Add(transaction);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return transactions;
        }

        public List<transactionTable> GetUserTransactions(int userID)
        {
            List<transactionTable> userTransactions = new List<transactionTable>();
            string connectionString = "Server=tcp:cldvpoe2.database.windows.net,1433;Initial Catalog=st10312227;Persist Security Info=False;User ID=kresen;Password=Naickerk94!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30"; // Load from configuration

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string sql = "SELECT transactionID, userID, productID, price, date FROM transactionTable WHERE userID = @userID";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@userID", userID);
                        con.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                transactionTable userTransaction = new transactionTable
                                {
                                    TransactionID = Convert.ToInt32(reader["TransactionID"]),
                                    UserID = Convert.ToInt32(reader["UserID"]),
                                    ProductID = Convert.ToInt32(reader["ProductID"]),
                                    Price = Convert.ToDecimal(reader["Price"]),
                                    Date = Convert.ToDateTime(reader["Date"])
                                };
                                userTransactions.Add(userTransaction);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                // Handle specific SQL exceptions
                // Log the exception or throw a custom exception
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                // Log the exception or throw a custom exception
            }

            return userTransactions;
        }



    }
}
