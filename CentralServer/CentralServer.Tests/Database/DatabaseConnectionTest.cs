using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CentralServer.Database;
using System.Data;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace CentralServer.Tests
{
    [TestClass]
    public class DatabaseConnectionTest
    {
        DatabaseConnection databaseConnection; 

        [TestMethod]
        public void ConnectTest()
        {
            databaseConnection = new DatabaseConnection();
            Assert.IsTrue(databaseConnection.Connect());
        }

        [TestMethod]
        public void ExecuteNonQueryTest()
        {
            databaseConnection = new DatabaseConnection();
            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(new MySqlParameter("@id", 2));
            parameters.Add(new MySqlParameter("@description", "test"));
            parameters.Add(new MySqlParameter("@amountVictims", 2));
            parameters.Add(new MySqlParameter("@amountWounded", 3));
            parameters.Add(new MySqlParameter("@long", 4));
            parameters.Add(new MySqlParameter("@lat", 5));
            parameters.Add(new MySqlParameter("@radius", 6));
            parameters.Add(new MySqlParameter("@danger", 7));

            int affectedRowsInsert = databaseConnection.ExecuteNonQuery("INSERT INTO Incident VALUES (@id, @description, @amountVictims ,@amountWounded, @long, @lat, @radius, @danger)", parameters);
            Assert.AreEqual(1, affectedRowsInsert);

            int affectedRowsDelete = databaseConnection.ExecuteNonQuery("DELETE FROM Incident WHERE id = @id", new MySqlParameter("@id", 2));
            Assert.AreEqual(1, affectedRowsDelete);
        }

        [TestMethod]
        public void ExecuteQuerySingleColumnTest()
        {
            databaseConnection = new DatabaseConnection();
            string[] columnNames = new string[1];
            columnNames[0] = "dangerLevel";

            List<string[]> dataSet = databaseConnection.ExecuteQuery("SELECT * FROM Incident WHERE id = @id", new MySqlParameter("@id", 1), columnNames);

            
            Assert.AreEqual(3, int.Parse(dataSet[0][0]));
        }

        [TestMethod]
        public void ExecuteQueryMultipleColumnsTest()
        {
            databaseConnection = new DatabaseConnection();
            string[] columnNames = new string[2];
            columnNames[0] = "id";
            columnNames[1] = "dangerLevel";

            List<string[]> dataSet = databaseConnection.ExecuteQuery("SELECT * FROM Incident WHERE id = @id", new MySqlParameter("@id", 1), columnNames);

            Assert.AreEqual(1, int.Parse(dataSet[0][0]));
            Assert.AreEqual(3, int.Parse(dataSet[0][1]));
        }

        [TestMethod]
        public void ExecuteQueryReader()
        {
            databaseConnection = new DatabaseConnection();
            var parameters = new List<MySqlParameter>();
            parameters.Add(new MySqlParameter("@id",1));
            var reader = databaseConnection.ExecuteQueryReader("SELECT mimeType, content FROM media WHERE id = @id LIMIT 1", parameters);
            Assert.IsTrue(reader.HasRows);
            reader.Read();
            Assert.AreEqual(reader.GetString("mimeType"), "text/plain");//mimetype is correct
            reader.Close();
            databaseConnection.Close();
            
        }
    }
}
