using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security;
using System.Security.Claims;
using System.Web;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Identity;
using Milestone2.Models;
using Newtonsoft.Json;
using Registration.Models;

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

        public (List<PlayerModel>, List<PlayerScoreModel>) GetAllUsers()
        {
            List<PlayerModel> players = new List<PlayerModel>();
            List<PlayerScoreModel> playerScores = new List<PlayerScoreModel>();
            PlayerModel p;

            using(var connection = ConnectToDb())
            {
                connection.Open();

                string sql = "select * from dbo.Player";

                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    //Executing query and initializing data reader
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)//Match found
                        {

                            while(dr.Read())
                            {
                                players.Add(new PlayerModel(dr["FirstName"].ToString(), dr["LastName"].ToString(), dr["Sex"].ToString(), dr["State"].ToString(), int.Parse(dr["Age"].ToString()), dr["Email"].ToString(), dr["Username"].ToString(), dr["Password"].ToString()));
                            }
                        }
                        else//No match found
                        {
                            players = null;
                        }
                    }
                }
                sql = "select * from dbo.UserScores";
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    //Executing query and initializing data reader
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)//Match found
                        {

                            while (dr.Read())
                            {
                                playerScores.Add(new PlayerScoreModel(dr["Username"].ToString(), int.Parse(dr["Clicks"].ToString()), int.Parse(dr["TimeTaken"].ToString())));
                            }
                        }
                        else//No match found
                        {
                            playerScores = null;
                        }
                    }
                }
            }
            return (players, playerScores);
        }

        public void SaveUserScore(PlayerScoreModel score)
        {
            using (var connection = ConnectToDb())
            {
                connection.Open();
                try
                {
                    String sql = "Insert into UserScores(USERNAME, CLICKS, TIMETAKEN) values(@USERNAME, @CLICKS, @TIMETAKEN)";

                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        //cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@USERNAME", score.Username);
                        cmd.Parameters.AddWithValue("@CLICKS", score.Clicks);
                        cmd.Parameters.AddWithValue("@TIMETAKEN", score.TimeTaken);

                        cmd.ExecuteNonQuery();
                    }
                        

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

        public static void WriteToXmlFile<T>(string filePath, T objectToWrite, bool append = false) where T : new()
        {
            using (StreamWriter file = File.CreateText(filePath))
            {
                JsonSerializer serializer = new JsonSerializer();
                //serialize object directly into file stream
                serializer.Serialize(file, objectToWrite);
            }
        }

        public static Board ReadFromXmlFile(String filePath)
        {
            if (File.Exists(filePath))
            {
                return JsonConvert.DeserializeObject<Board>(File.ReadAllText(filePath));
            }
            else
            {
                return null;
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
