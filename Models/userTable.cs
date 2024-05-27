using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Reflection.Metadata.Ecma335;


namespace KhumaloCraftPOE.Models
{
    public class userTable
    {


        public static string con_string = "Server=tcp:cldvpoe2.database.windows.net,1433;Initial Catalog=st10312227;Persist Security Info=False;User ID=kresen;Password=Naickerk94!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30";

        public static SqlConnection con = new SqlConnection(con_string);


        public int UserID { get; set; }
        public string Name { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }





        public int insert_User(userTable m)
        {

            try
            {
                string sql = "INSERT INTO userTable (userName, userSurname, userEmail) VALUES (@Name, @Surname, @Email)";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@Name", m.Name);
                cmd.Parameters.AddWithValue("@Surname", m.Surname);
                cmd.Parameters.AddWithValue("@Email", m.Email);
                con.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                con.Close();
                return rowsAffected;
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }
        public List<userTable> GetUsers()
        {
            List<userTable> users = new List<userTable>();

            try
            {
                string sql = "SELECT * FROM userTable";
                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    userTable user = new userTable();
                    user.UserID = (int)reader["userID"];
                    user.Name = reader["userName"].ToString();
                    user.Surname = reader["userSurname"].ToString();
                    user.Email = reader["userEmail"].ToString();
                    users.Add(user);
                }
                reader.Close();
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return users;
        }

    }
}