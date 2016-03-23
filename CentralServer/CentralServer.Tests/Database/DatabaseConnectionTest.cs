﻿using System;
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
            parameters.Add(new MySqlParameter("@amountVictims", 2));
            parameters.Add(new MySqlParameter("@amountWounded", 3));
            parameters.Add(new MySqlParameter("@long", 4));
            parameters.Add(new MySqlParameter("@lat", 5));
            parameters.Add(new MySqlParameter("@radius", 6));
            parameters.Add(new MySqlParameter("@danger", 7));

            int affectedRowsInsert = databaseConnection.ExecuteNonQuery("INSERT INTO Incident VALUES (@id, @amountVictims ,@amountWounded, @long, @lat, @radius, @danger)", parameters);
            Assert.AreEqual(1, affectedRowsInsert);

            int affectedRowsDelete = databaseConnection.ExecuteNonQuery("DELETE FROM Incident WHERE id = @id", new MySqlParameter("@id", 2));
            Assert.AreEqual(1, affectedRowsDelete);
        }

        [TestMethod]
        public void ExecuteQueryTest()
        {
            databaseConnection = new DatabaseConnection();
            MySqlDataReader reader = databaseConnection.ExecuteQuery("SELECT * FROM Incident WHERE id = @id", new MySqlParameter("@id", 1));
            while (reader.Read())
            {
                int dangerLevel = reader.GetInt32(6);
                Assert.AreEqual(3, dangerLevel);
            }
            reader.Close();
            databaseConnection.Connection.Close();
        }
    }
}