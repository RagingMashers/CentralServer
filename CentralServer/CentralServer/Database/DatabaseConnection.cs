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
        private MySqlConnection connection = null;

        public MySqlConnection Connection { get { return connection; }}

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

        /*
        /// <summary>
        /// Executes an query on the database. Use a while(reader.Read()) loop to iterate through the reader. 
        /// Close the reader and the connection after you are done!
        /// </summary>
        /// <param name="query">The SQL query</param>
        /// <param name="parameters">The list with MySqlParamters for preventing SQL-Injection</param>
        /// <returns> If no rows found it returns null, else it return a MySqlDataReader</returns>
        /// 
        */

        public List<string[]> ExecuteQuery(string query, List<MySqlParameter> parameters, string[] columnNames)
        {
            try
            {
                if (Connect())
                {
                    List<string[]> dataSet = new List<string[]>();

                    MySqlCommand cmd = connection.CreateCommand();
                    cmd.CommandText = query;
                    foreach(MySqlParameter parameter in parameters){
                        cmd.Parameters.Add(parameter);
                    }

                    MySqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        string[] dataRow = new string[columnNames.Length];

                        while (reader.Read())
                        {
                            int count = 0;

                            foreach (string columnName in columnNames)
                            {
                                dataRow[count] = reader.GetString(columnName);
                                count++;
                            }

                            dataSet.Add(dataRow);
                        }
                        reader.Close();

                        return dataSet;
                    }
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
            return null;
        }

        /// <summary>
        /// Method for the ExecuteQuery with only one parameter
        /// </summary>
        /// <param name="query">The query that will be executed</param>
        /// <param name="parameter">The paramter that will prevent SQL-injection</param>
        /// <returns></returns>
        public List<string[]> ExecuteQuery(string query, MySqlParameter parameter, string[] columnNames)
        {
            List<MySqlParameter> parameters = new List<MySqlParameter>();
            parameters.Add(parameter);
            return ExecuteQuery(query, parameters, columnNames);
        }

        /// <summary>
        /// Insert/delete data into/from the database
        ///
        /// </summary>
        /// <param name="query">The insert/delete query</param>
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

        /// <summary>
        /// Insert / delete data into/from the database.
        /// </summary>
        /// <param name="query">The insert/delete query</param>
        /// <param name="parameter">The parameter used for preventing SQL injection</param>
        /// <returns></returns>
        public int ExecuteNonQuery(string query, MySqlParameter parameter)
        {
            List<MySqlParameter> parameters = new List<MySqlParameter>();
            parameters.Add(parameter);
            return ExecuteNonQuery(query, parameters);
        }
    }
}