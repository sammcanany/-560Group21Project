using Group21ProjectMVC.ViewModels;
using System.Data.SqlClient;

namespace Group21ProjectMVC.Helpers
{
    public class CommonHelper
    {
        private IConfiguration _configuration;
        public CommonHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public UserViewModel GetUserByEmail(string query)
        {
            UserViewModel user = new UserViewModel();
            string connectionString = _configuration["ConnectionStrings:DefaultConnection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = query;
                SqlCommand cmd = new SqlCommand(sql, connection);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        user.Id = Convert.ToInt32(reader["ProfileID"]);
                        user.FirstName = reader["FirstName"].ToString();
                        user.LastName = reader["LastName"].ToString();
                        user.Email = reader["Email"].ToString();
                        user.PhoneNumber = reader["PhoneNumber"].ToString();
                        user.Password = reader["Password"].ToString();

                    }
                }
                connection.Close();
            }
            return user;
        }

        public int DMLTransaction(string Query)
        {
            int Result;
            string connectionString = _configuration["ConnectionStrings:DefaultConnection"];
            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = Query;
                SqlCommand cmd = new SqlCommand(sql, connection);
                Result = cmd.ExecuteNonQuery();
                connection.Close();
            }
            return Result;
        }

        public bool UserAlreadyExists(string Query)
        {
            bool Result = false;
            string connectionString = _configuration["ConnectionStrings:DefaultConnection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = Query;
                SqlCommand cmd = new SqlCommand(sql, connection);
                SqlDataReader reader = cmd.ExecuteReader();
                if(reader.HasRows) Result = true;
                connection.Close();
            }
            return Result;
        }
    }
}
