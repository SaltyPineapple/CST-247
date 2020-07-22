using Registration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

/* Mark Pratt
 * 
 */

namespace Registration.Services {
    public class Security {

        //SqlConnection connection = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Players;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");


        public bool existingUser(PlayerModel model) {

            using (var connection = ConnectToDb())
            {
                connection.Open();
                try
                {
                    string sql = "select * from dbo.Player where Username = @username";
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        //Adding params to sql query
                        cmd.Parameters.AddWithValue("@username", model.Username);

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

                catch (Exception ex)
                {
                    if (connection.State == System.Data.ConnectionState.Open)
                    {
                        connection.Close();
                    }

                    Console.WriteLine(ex.Message.ToString());
                    return true;
                }
            }

        }

        public void addNewUser(PlayerModel model) {

            using (var connection = ConnectToDb())
            {
                connection.Open();
                try
                {
                    String sql = "Insert into Player(FIRSTNAME, LASTNAME, SEX, AGE, STATE, EMAIL, USERNAME, PASSWORD) values(@FIRSTNAME, @LASTNAME, @SEX, @AGE, @STATE, @EMAIL, @USERNAME, @PASSWORD)";

                    SqlCommand cmd = new SqlCommand(sql, connection);
                    //cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FIRSTNAME", model.FirstName);
                    cmd.Parameters.AddWithValue("@LASTNAME", model.LastName);
                    cmd.Parameters.AddWithValue("@SEX", model.Sex);
                    cmd.Parameters.AddWithValue("@AGE", model.Age);
                    cmd.Parameters.AddWithValue("@STATE", model.State);
                    cmd.Parameters.AddWithValue("@EMAIL", model.Email);
                    cmd.Parameters.AddWithValue("@USERNAME", model.Username);
                    cmd.Parameters.AddWithValue("@PASSWORD", model.Password);

                    cmd.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    if (connection.State == System.Data.ConnectionState.Open)
                    {
                        connection.Close();
                    }

                    Console.WriteLine(ex.Message.ToString());

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
