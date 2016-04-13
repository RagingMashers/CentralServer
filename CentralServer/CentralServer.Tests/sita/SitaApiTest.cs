using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CentralServer.Database;
using System.Collections.Generic;
using CentralServer.sita;
using MySql.Data.MySqlClient;

namespace CentralServer.Tests.sita
{
    [TestClass]
    public class SitaApiTest
    {
        SitaApi sitaApi = new SitaApi();
        DatabaseConnection databaseConnection = new DatabaseConnection();

        [TestMethod]
        public void GetToxicationsTest()
        {
            Toxication[] dataSet = sitaApi.GetToxications(null);

            Boolean result = false;
            if (dataSet.Length >= 1)
            {
                result = true;
            }

            Assert.IsNotNull(dataSet);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void AddToxicationTest()
        {

            Boolean succes = sitaApi.AddToxication(null, "TestGif", "Testbeschrijving", "O213", 4, 1.0);
            Assert.IsTrue(succes);

            int affectedRowsDelete = databaseConnection.ExecuteNonQuery("DELETE FROM toxication WHERE name = @name", new MySqlParameter("@name", "TestGif"));
            Assert.AreEqual(1, affectedRowsDelete);
        }

        [TestMethod]
        public void SendMessage()
        {
            Boolean succes = sitaApi.SendMessage(null, 1, "ditIsEenTestBericht");
            Assert.IsTrue(succes);

            int affectedRowsDelete = databaseConnection.ExecuteNonQuery("DELETE FROM message WHERE description = @description", new MySqlParameter("@description", "ditIsEenTestBericht"));
            Assert.AreEqual(1, affectedRowsDelete);
        }

        [TestMethod]
        public void SendMessageWithMedia()
        {
            Boolean succes = sitaApi.SendMessageWithMedia(null, 1, "ditIsEenTweedeTestBericht", 1);
            Assert.IsTrue(succes);

            int affectedRowsDelete = databaseConnection.ExecuteNonQuery("DELETE FROM message WHERE description = @description", new MySqlParameter("@description", "ditIsEenTweedeTestBericht"));
            Assert.AreEqual(1, affectedRowsDelete);
        }
        
        [TestMethod]
        public void AddIncidentTest()
        {
            bool succes = sitaApi.AddIncident("testToken", 1, 1, 4.1, 1.0, 6, 6, "Grote brand");
            Assert.IsTrue(succes);
        }

        [TestMethod]
        public void EditIncidentTest()
        {
            DatabaseConnection connection = new DatabaseConnection();
            List<MySqlParameter> parameters = new List<MySqlParameter>();

            string[] columnNames = new string[1];
            columnNames[0] = "MAX(id)";

            parameters.Add(new MySqlParameter("@null", ""));

            bool succes = sitaApi.EditIncident("testToken", int.Parse(connection.ExecuteQuery("SELECT MAX(id) FROM Incident", parameters, columnNames)[0][0]), 1, 1, 4.1, 1.0, 6, 6, "Extreem Grote brand TEST");
            Assert.IsTrue(succes);
        }

        [TestMethod]
        public void GetIncidentTest()
        {
            Incident[] incidents = sitaApi.GetIncidents("testToken", 1, 0);
            Assert.AreEqual(1, incidents.Length);
        }


        [TestMethod]
        public void DeleteIncidentTest()
        {
            DatabaseConnection connection = new DatabaseConnection();
            List<MySqlParameter> parameters = new List<MySqlParameter>();

            string[] columnNames = new string[1];
            columnNames[0] = "MAX(id)";

            parameters.Add(new MySqlParameter("@null", ""));

            bool succes = sitaApi.DeleteIncident("testToken", int.Parse(connection.ExecuteQuery("SELECT MAX(id) FROM Incident", parameters, columnNames)[0][0]));
            Assert.IsTrue(succes);
        }

        [TestMethod]
        public void GetTeamsNearIncidentTest()
        {
            // INSERT TEST ROWS
            bool succes = sitaApi.AddIncident(null, 2, 12, 5.85757, 51.19417, 1, 400, "TestTerroristische aanslag");
            Assert.IsTrue(succes);

            int rowsAffected = databaseConnection.ExecuteNonQuery("INSERT INTO Team (type, startDate, endDate, longitude, latitude) VALUES (2, '1900-03-06', null, 5.82840, 51.21805)", new MySqlParameter());
            Assert.AreEqual(1, rowsAffected);

            rowsAffected = 0;
            rowsAffected = databaseConnection.ExecuteNonQuery("INSERT INTO Team (type, startDate, endDate, longitude, latitude) VALUES (3, '1900-03-06', null, 	4.89517, 52.37022)", new MySqlParameter());
            Assert.AreEqual(1, rowsAffected);

            // TEST
            Team[] dataSet = sitaApi.GetTeamsNearIncident(5.85757, 51.19417);

            Boolean result = false;
            if (dataSet.Length == 1)
            {
                result = true;
            }

            Assert.IsNotNull(dataSet);
            Assert.IsTrue(result);

            // DELETE TEST ROWS
            rowsAffected = 0;
            rowsAffected = databaseConnection.ExecuteNonQuery("DELETE FROM Incident WHERE description = 'TestTerroristische aanslag'", new MySqlParameter());
            Assert.AreEqual(1, rowsAffected);

            rowsAffected = 0;
            rowsAffected = databaseConnection.ExecuteNonQuery("DELETE FROM Team WHERE longitude = 5.82840 AND startDate = '1900-03-06'", new MySqlParameter());
            Assert.AreEqual(1, rowsAffected);

            rowsAffected = 0;
            rowsAffected = databaseConnection.ExecuteNonQuery("DELETE FROM Team WHERE longitude = 4.89517 AND startDate = '1900-03-06'", new MySqlParameter());
            Assert.AreEqual(1, rowsAffected);
        }
    }
}