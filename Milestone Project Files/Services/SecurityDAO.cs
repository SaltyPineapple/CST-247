using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security;
using System.Security.Claims;
using System.Web;
using Microsoft.AspNetCore.Identity;
using Milestone2.Models;

/* Patrick Garcia
 * 
 */

namespace Milestone2.Services.Data
{
    public class SecurityDAO
    {
        //Uses a sql connection to query database for login validation
        public bool FindByUser(LoginModel user)
        { 
            using (var connection = ConnectToDb())//getting database connection
            {
                connection.Open();

                //Sql query for login validation
                string sql = "select * from dbo.Player where UserName = @username and Password = @password";
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    //Adding params to sql query
                    cmd.Parameters.AddWithValue("@username", user.UserName);
                    cmd.Parameters.AddWithValue("@password", user.Password);

                    //Executing query and initializing data reader
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)//Match found
                        {
                            return true;
                        }
                        else//No match found
                        {
                            return false;
                        }
                    }
                }
            }
        }

        //This method returns a SQL connection to a database
        public SqlConnection ConnectToDb()
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=MinesweeperDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            return connection;
        }
    }
}
