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
        DatabaseConnection connection; 

        [TestMethod]
        public void ConnectTest()
        {
            connection = new DatabaseConnection();
            Assert.IsTrue(connection.Connect());
        }

        [TestMethod]
        public void ExecuteNonQueryTest()
        {
            connection = new DatabaseConnection();
            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(new MySqlParameter("@id", 2));
            parameters.Add(new MySqlParameter("@amountVictims", 2));
            parameters.Add(new MySqlParameter("@amountWounded", 3));
            parameters.Add(new MySqlParameter("@long", 4));
            parameters.Add(new MySqlParameter("@lat", 5));
            parameters.Add(new MySqlParameter("@radius", 6));
            parameters.Add(new MySqlParameter("@danger", 7));

            int affectedRowsInsert = connection.ExecuteNonQuery("INSERT INTO Incident VALUES (@id, @amountVictims ,@amountWounded, @long, @lat, @radius, @danger)", parameters);
            Assert.AreEqual(1, affectedRowsInsert);

            int affectedRowsDelete = connection.ExecuteNonQuery("DELETE FROM Incident WHERE id = @id", parameters);
            Assert.AreEqual(1, affectedRowsDelete);
        }
    }
}
