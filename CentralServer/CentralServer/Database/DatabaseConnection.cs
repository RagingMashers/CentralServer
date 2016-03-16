using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace CentralServer.Database
{
    public class DatabaseConnection
    {
        private string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        private string con = "Server=athena01.fhict.local;Database=dbi318908;Uid=dbi318908;Pwd=proftaak2016;";
        MySqlConnection connection = null;

        public DatabaseConnection()
        {
            Connect();
        }

        

        /// <summary>
        /// Connects to the database connection.
        /// </summary>
        public bool Connect()
        {
            try
            {
                connection = new MySqlConnection();
                connection.ConnectionString = constr;
                connection.Open();

                return true;
            }
            catch
            {
                return false;
            }
            
        }

        /// <summary>
        /// Closes the database connection.
        /// </summary>
        public void Close()
        {
            if (connection != null)
            {
                connection.Close();
            }
        }

        /// <summary>
        /// Executes an query on the database and returns an string array.
        /// </summary>
        /// <param name="query">The query</param>
        //public string ExecuteQuery(string query)
        //{
        //    try
        //    {
        //            if (Connect())
        //            {
        //                MySqlCommand cmd = connection.CreateCommand();
        //                cmd.CommandText = query;

        //                MySqlDataReader Reader = cmd.ExecuteReader();
        //                if (!Reader.HasRows) return null;
        //                while (Reader.Read())
        //                {         
        //                    return Reader["id"].ToString();
        //                }
        //                Reader.Close();
        //            }
        //    }
        //    catch (MySqlException ex)
        //    {
        //        Console.WriteLine(ex.ToString());
        //    }
        //    finally
        //    {
        //        if (connection.State == ConnectionState.Open)
        //        {
        //            Close();
        //        }
        //    }
        //    return null;
        //}

        /// <summary>
        /// Insert/delete data into/from the database
        ///
        /// </summary>
        /// <param name="query">The insert query</param>
        /// <param name="parameters">The parameters used for preventing SQL injection</param>
        /// <returns>Returns the number of affected rows.</returns>
        public int ExecuteNonQuery(string query, List<MySqlParameter> parameters)
        {
            try
            {
                if (Connect())
                {
                    MySqlCommand cmd = connection.CreateCommand();
                    cmd.CommandText = query;
                    foreach(MySqlParameter parameter in parameters){
                        cmd.Parameters.Add(parameter);
                    }

                    return cmd.ExecuteNonQuery();
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    Close();
                }
            }
            return 0;
        }
    }
}