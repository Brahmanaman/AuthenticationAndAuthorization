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
    }
}