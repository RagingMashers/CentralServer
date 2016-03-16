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
        [TestMethod]
        public void ConnectTest()
        {
            DatabaseConnection connection = new DatabaseConnection();
            Assert.IsTrue(connection.Connect());
        }

        [TestMethod]
        public void InsertTest()
        {
            DatabaseConnection connection = new DatabaseConnection();
            List<MySqlParameter> parameters = new List<MySqlParameter>();
            parameters.Add(new MySqlParameter("@id", 2));
            parameters.Add(new MySqlParameter("@amountVictims", 2));
            parameters.Add(new MySqlParameter("@amountWounded", 3));
            parameters.Add(new MySqlParameter("@long", 4));
            parameters.Add(new MySqlParameter("@lat", 5));
            parameters.Add(new MySqlParameter("@radius", 6));
            parameters.Add(new MySqlParameter("@danger", 7));

            int affectedRows = connection.Insert("INSERT INTO Incident VALUES (@id, @amountVictims ,@amountWounded, @long, @lat, @radius, @danger)", parameters);
            Assert.AreEqual(1, affectedRows);
        }
    }
}
