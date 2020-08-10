using Activity1Part3.Controllers;
using Activity1Part3.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace Activity1Part3.Services.Data {
    public class SecurityDAO {
        public bool FindByUser(UserModel user) {

            string con = "Data source = (localdb)\\MSSQLLocalDB; " +
                "Initial Catalog=Test;Integrated Security = true";

            using (SqlConnection connection = new SqlConnection(con)) {

                SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.Users", connection);
                
                SqlDataReader reader = null;

                try {
                    connection.Open();
                    Console.WriteLine("CONN SUCCESSFUL");

                    reader = cmd.ExecuteReader();

                    bool isTrue = false;
                    while (reader.Read()) {
                        string username = (string)reader["USERNAME"];
                        string password = (string)reader["PASSWORD"];
                        if(username == user.Username && password == user.Password) {
                            isTrue = true;
                        }
                        
                    }
                    if (isTrue) {
                        return true;
                    }
                    else {
                        return false;
                    }
                        
                }
                catch (Exception ex) {
                    Console.WriteLine(ex.Message);
                    return false;
                }
                finally {
                    if(reader != null) {
                        reader.Close();
                    }
                    connection.Close();
                }
                

            }


            
            
        }
    
    }
}