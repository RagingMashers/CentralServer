using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace CentralServer.sita
{
    public class DatabaseConnection
    {
        private static string constr = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
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
                connection = new MySqlConnection(constr);
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
        public DataSet ExecuteQuery(String query)
        {
            if(Connect()){
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = query;

                DataSet dataSet = new DataSet();
                MySqlDataAdapter adp = new MySqlDataAdapter(command);
                adp.Fill(dataSet);

                return dataSet;
            }
            else
            {
                return null;
            }
        }
    }
}