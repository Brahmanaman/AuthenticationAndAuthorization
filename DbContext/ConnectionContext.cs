using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using AuthenticationAndAuthorization.Models;

namespace AuthenticationAndAuthorization.DbContext
{
    public class ConnectionContext
    {
        string ConnectionString = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
        public bool RegisterUser(SignUp signupUser)
        {
            int result = 0;
            try
            {
                SqlConnection con = new SqlConnection(ConnectionString);
                SqlCommand cmd = new SqlCommand("spRegisterUser", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@name", signupUser.Name);
                cmd.Parameters.AddWithValue("@email", signupUser.Email);
                cmd.Parameters.AddWithValue("@password", signupUser.Password);
                cmd.Parameters.AddWithValue("@confirmpassword", signupUser.ConfirmPassword);
                con.Open();
                result = cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            if (result > 0)
            {
                return true;
            }
            return false;
        }

        public List<Login> LoginUser()
        {

            List<Login> login = new List<Login>();
            try
            {
                DataTable dt = new DataTable();
                SqlConnection con = new SqlConnection(ConnectionString);
                //SqlCommand cmd = new SqlCommand("spLoginUser", con);
                SqlDataAdapter da = new SqlDataAdapter("spLoginUser", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.Fill(dt);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Login loginUser = new Login()
                    {
                        UserId = Convert.ToInt32(dt.Rows[i][0]),
                        UserName = Convert.ToString(dt.Rows[i][1]),
                        Email = Convert.ToString(dt.Rows[i][2]),
                        Password = Convert.ToString(dt.Rows[i][3]),
                    };

                    login.Add(loginUser);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return login;
        }
    }
}