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

        SqlConnection connection = new SqlConnection("Data source = (localdb)\\MSSQLLocalDB; " +
               "Initial Catalog=Players;Integrated Security = true");


        public bool existingUser(PlayerModel model) {

            connection.Open();

            try {
                string sql = "select * from dbo.Player where Username = @username";
                using (SqlCommand cmd = new SqlCommand(sql, connection)) {
                    //Adding params to sql query
                    cmd.Parameters.AddWithValue("@username", model.Username);

                    //Executing query and initializing data reader
                    using (SqlDataReader dr = cmd.ExecuteReader()) {
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
            catch (Exception ex) {
                if (connection.State == System.Data.ConnectionState.Open) {
                    connection.Close();
                }

                Console.WriteLine(ex.Message.ToString());
                return true;
            }
        }

        public void addNewUser(PlayerModel model) {

            try { 
                SqlCommand cmd = new SqlCommand("AddPlayer", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FIRSTNAME", model.FirstName);
                cmd.Parameters.AddWithValue("@LASTNAME", model.LastName);
                cmd.Parameters.AddWithValue("@SEX", model.Sex);
                cmd.Parameters.AddWithValue("@AGE", model.Age);
                cmd.Parameters.AddWithValue("@STATE", model.State);
                cmd.Parameters.AddWithValue("@EMAIL", model.Email);
                cmd.Parameters.AddWithValue("@USERNAME", model.Username);
                cmd.Parameters.AddWithValue("@PASSWORD", model.Password);

                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();


            }
            catch(Exception ex) {
                if(connection.State == System.Data.ConnectionState.Open) {
                    connection.Close();
                }
               
                Console.WriteLine(ex.Message.ToString());
                
            }

        }
    }
}