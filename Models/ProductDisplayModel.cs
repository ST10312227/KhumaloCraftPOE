using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace KhumaloCraftPOE.Models
{
    public class ProductDisplayModel
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public String ProductPrice { get; set; }
        public string ProductCategory { get; set; }
        public String ProductAvailability { get; set; }

        public ProductDisplayModel() { }

        //Parameterized Constructor: This constructor takes five parameters (id, name, price, category, availability) and initializes the corresponding properties of ProductDisplayModel with the provided values.
        public ProductDisplayModel(int id, string name, string price, string category, String availability)
        {
            ProductID = id;
            ProductName = name;
            ProductPrice = price;
            ProductCategory = category;
            ProductAvailability = availability;
        }

        public static List<ProductDisplayModel> SelectProducts()
        {
            List<ProductDisplayModel> products = new List<ProductDisplayModel>();

            string con_string = "Server=tcp:cldvpoe2.database.windows.net,1433;Initial Catalog=st10312227;Persist Security Info=False;User ID=kresen;Password=Naickerk94!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30";

            using (SqlConnection con = new SqlConnection(con_string))
            {
                string sql = "SELECT productID, productName, productPrice, productCategory, productAvailability FROM productTable";
                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ProductDisplayModel product = new ProductDisplayModel();
                    product.ProductID = Convert.ToInt32(reader["productID"]);
                    product.ProductName = reader["productName"].ToString();
                    product.ProductPrice = reader["productPrice"].ToString();
                    product.ProductCategory = reader["productCategory"].ToString();
                    product.ProductAvailability = reader["productAvailability"].ToString();
                    products.Add(product);
                }
                reader.Close();
            }
            return products;
        }
    }
}