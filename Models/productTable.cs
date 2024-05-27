using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace KhumaloCraftPOE.Models
{
    public class productTable
    {
        public static string con_string = "Server=tcp:cldvpoe2.database.windows.net,1433;Initial Catalog=st10312227;Persist Security Info=False;User ID=kresen;Password=Naickerk94!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30";
        public static SqlConnection con = new SqlConnection(con_string);
        public int ProductID { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public string Category { get; set; }
        public string Availability { get; set; }

        public int insert_product(productTable p)
        {
            try
            {
                string sql = "INSERT INTO productTable (productName, productPrice, productCategory, productAvailability) VALUES (@Name, @Price, @Category, @Availability)";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@Name", p.Name);
                cmd.Parameters.AddWithValue("@Price", p.Price);
                cmd.Parameters.AddWithValue("@Category", p.Category);
                cmd.Parameters.AddWithValue("@Availability", p.Availability);
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

        public List<productTable> GetProducts()
        {
            List<productTable> products = new List<productTable>();

            try
            {
                string sql = "SELECT ProductID, productName, productPrice, productCategory, productAvailability FROM productTable";
                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    productTable product = new productTable();
                    product.ProductID = Convert.ToInt32(reader["ProductID"]);
                    product.Name = reader["productName"].ToString();
                    product.Price = reader["productPrice"].ToString();
                    product.Category = reader["productCategory"].ToString();
                    product.Availability = reader["productAvailability"].ToString();
                    products.Add(product);
                }
                reader.Close();
                con.Close();
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                // For now, rethrow the exception
                throw ex;
            }

            return products;
        }
    }
}