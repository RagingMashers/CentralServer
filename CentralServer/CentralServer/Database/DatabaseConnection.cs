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
        private readonly string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        private MySqlConnection connection = null;

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
            connection?.Close();
        }

        /// <summary>
        /// Executes an query on the database. 
        /// The data returned will be a list of rows of data.
        /// </summary>
        /// <param name="query">The SQL query.</param>
        /// <param name="parameters">The list with MySqlParamters for preventing SQL-Injection.</param>
        /// <param name="columnNames">An array of strings that represent the columnnames you want returned.</param>
        /// <returns>If no rows found it returns null, else it returns a dataSet(List of string arrays).</returns>
        public List<string[]> ExecuteQuery(string query, List<MySqlParameter> parameters, string[] columnNames)
        {
            try
            {
                if (Connect())
                {
                    var dataSet = new List<string[]>();

                    var cmd = connection.CreateCommand();
                    cmd.CommandText = query;
                    cmd.Parameters.AddRange(parameters.ToArray());

                    var reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        var dataRow = new string[columnNames.Length];

                        while (reader.Read())
                        {
                            var count = 0;

                            foreach (var columnName in columnNames)
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
        /// Executes an query on the database. 
        /// The data returned will be a list of rows of data.
        /// </summary>
        /// <param name="query">The SQL query.</param>
        /// <param name="parameters">The list with MySqlParamters for preventing SQL-Injection.</param>
        /// <returns>The reader object of the query</returns>
        public MySqlDataReader ExecuteQueryReader(string query, List<MySqlParameter> parameters)
        {
            try
            {
                if (Connect())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = query;
                    cmd.Parameters.AddRange(parameters.ToArray());

                    return cmd.ExecuteReader();
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                
            }
            return null;
        }

        /// <summary>
        /// Executes an query on the database. 
        /// The object returned will be the first column of the first row of the query result.
        /// </summary>
        /// <param name="query">The SQL query.</param>
        /// <param name="parameters">The list with MySqlParamters for preventing SQL-Injection.</param>
        /// <returns>The first column of the first row or null</returns>
        public object ExecuteScalar(string query, List<MySqlParameter> parameters)
        {
            try
            {
                if (Connect())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = query;
                    cmd.Parameters.AddRange(parameters.ToArray());

                    var result = cmd.ExecuteScalar();
                
                    return result is DBNull? null : result;

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
            var parameters = new List<MySqlParameter> {parameter};
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
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = query;
                    cmd.Parameters.AddRange(parameters.ToArray());

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
            var parameters = new List<MySqlParameter> {parameter};
            return ExecuteNonQuery(query, parameters);
        }
    }
}