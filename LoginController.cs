using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace WebApp_Test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public LoginController(IConfiguration configuration)
        {
            _configuration = configuration;

        }
        [HttpGet]
        [Route("VerifyMachine")]
        public string VerifyMachine(string MCNId)
        {
            string result = string.Empty;

            using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DbCon")))
            {
                conn.Open();

                string query = "SELECT * FROM TBL_HHC WHERE TBL_MCN_ID = '"+@MCNId+"'";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MCNId", MCNId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Assuming you're interested in some field, for example, a column named "Name".
                            result = reader["TBL_HHC_ID"].ToString();
                        }
                    }
                }
            }
            return result;
        }

        [HttpGet]
        [Route("Login")]
        public string Login(string userId,string password)
        {
            string result = string.Empty;

            using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DbCon")))
            {
                conn.Open();

                string query = "SELECT * FROM TBL_USER WHERE USER_NAMES = '"+@userId+"' AND USERPASWRD = '"+@password+"'";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@userId", userId);
                    cmd.Parameters.AddWithValue("@password", password);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Assuming you're interested in some field, for example, a column named "Name".
                            result = reader["TBL_USERID"].ToString();
                        }
                    }
                }
            }
            return result;
        }

    }

}
