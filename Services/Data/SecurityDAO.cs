using BibleVerseSearchApp.Models;
using NLog;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security;
using System.Web;

/* Mark Pratt
 * 8/12/2020
 */

namespace BibleVerseSearchApp.Services.Data {
    public class SecurityDAO {

        // logger
        private static Logger logger = LogManager.GetLogger("myAppLoggerRules");

        // Connection string for database connection
        string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Verses;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        // Used to return verse from database
        Verse foundVerse = new Verse();

        // List used to validate book of entered verse
        List<String> oldTestament = new List<String> { "genesis", "exodus", "leviticus", "numbers", "deuteronomy", "joshua", "judges", "juth", "1 samuel", "2 samuel", "1 kings", "2 kings", "1 chronicles", "2 chronicles", "ezra", "nehemiah", "esther", "job", "psalms", "proverbs", "ecclesiastes", "song of Solomon", "isaiah", "jeremiah", "lamentations", "ezekiel", "daniel", "hosea", "joel", "amos", "obadiah", "jonah", "micah", "nahum", "habakkuk", "zephaniah", "haggai", "zechariah", "malachi" };

        List<String> newTestament = new List<String> { "matthew", "mark", "luke", "john", "acts", "romans", "1 corinthians", "2 corinthians", "galatians", "ephesians", "philippians", "colossians", "1 thesselonians", "2 thesselonians", "1 timothy", "2 timothy", "titus", "philemon", "hebrews", "james", "1 peter", "2 peter", "1 john", "2 john", "3 john", "jude", "revelation"};

        // Method: Find verse
        // Used to find a verse within the databse
        // Returns a bool value
        // also sets the properties of variable foundVerse

        internal bool FindVerse(Verse verse) {
            
            // Start query by assuming nothing is there
            bool success = false;

            // write sql expression

            string queryString = "SELECT * FROM dbo.Verse WHERE TESTAMENT = @Testament AND BOOK = @Book AND CHAPTER = @Chapter AND VERSENUM = @VerseNumber";

            // Create and open the connection
            using (SqlConnection connection = new SqlConnection(connectionString)) {
                // Create the command
                SqlCommand cmd = new SqlCommand(queryString, connection);
                cmd.Parameters.Add("@Testament", System.Data.SqlDbType.VarChar, 15).Value = verse.Testament;
                cmd.Parameters.Add("@Book", System.Data.SqlDbType.VarChar, 30).Value = verse.Book;
                cmd.Parameters.Add("@Chapter", System.Data.SqlDbType.Int).Value = verse.Chapter;
                cmd.Parameters.Add("@VerseNumber", System.Data.SqlDbType.Int).Value = verse.VerseNumber;


                try {
                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    
                    // if something found in database, set success to true and set returnVerse properties
                    if (reader.HasRows) {
                        success = true;
                        while (reader.Read()) {
                            foundVerse.Testament = reader.GetString(1);
                            foundVerse.Book = reader.GetString(2);
                            foundVerse.Chapter = reader.GetInt32(3);
                            foundVerse.VerseNumber = reader.GetInt32(4);
                            foundVerse.BibleVerse = reader.GetString(5);
                            
                        }
                    }
                    else {
                        success = false;
                    }
                    reader.Close();
                }
                catch(Exception ex) {
                    Console.WriteLine(ex.Message);
                }

            }
            return success;
        
        }


        //Method: Enter Verse
        // Enters a verse into the database
        // returns a bool value

        public bool EnterVerse(Verse verse) {
            bool success = false;

            using (SqlConnection connection = new SqlConnection(connectionString)) {
                connection.Open();
                try {

                    String queryString = "INSERT INTO dbo.Verse (TESTAMENT, BOOK, CHAPTER, VERSENUM, VERSE) VALUES (@Testament, @book, @Chapter, @VerseNumber, @BibleVerse)";

                    SqlCommand cmd = new SqlCommand(queryString, connection);
                    cmd.Parameters.AddWithValue("@Testament", verse.Testament);
                    cmd.Parameters.AddWithValue("@Book", verse.Book);
                    cmd.Parameters.AddWithValue("@Chapter", verse.Chapter);
                    cmd.Parameters.AddWithValue("@VerseNumber", verse.VerseNumber);
                    cmd.Parameters.AddWithValue("@BibleVerse", verse.BibleVerse);


                    // Block is used to validate testament selection and book selection
                    // only executes if old or new testament is input and if a valid book from those testaments is input
                    if(!verse.Testament.ToLower().Equals("old") && !verse.Testament.ToLower().Equals("new")){
                        logger.Info("Invalid Testament");
                    }
                    else {
                        if (verse.Testament.ToLower().Equals("old")) {
                            for(int x=0; x< oldTestament.Count; x++) {
                                if (verse.Book.ToLower().Equals(oldTestament[x])) {
                                    logger.Info("Valid Testament and Book");
                                    cmd.ExecuteNonQuery();
                                    success = true;
                                    break;
                                }
                            }
                        }
                        else {
                            for (int x = 0; x < newTestament.Count; x++) {
                                if (verse.Book.ToLower().Equals(newTestament[x])) {
                                    logger.Info("Valid Testament and Book");
                                    cmd.ExecuteNonQuery();
                                    success = true;
                                    break;
                                }
                            }
                        }
                        
                        
                    }

                }
                catch (Exception ex) {
                    if (connection.State == System.Data.ConnectionState.Open) {
                        connection.Close();
                    }

                    Console.WriteLine(ex.Message.ToString());
                }


            }

            return success;

        }

        public Verse ReturnVerse() {
            return foundVerse;
        }
    }
}