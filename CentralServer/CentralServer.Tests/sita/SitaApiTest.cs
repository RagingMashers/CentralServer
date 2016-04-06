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

            bool result = false;
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
            bool succes = sitaApi.AddToxication(null, "TestGif", "Testbeschrijving", "O213", 4, 1.0);
            Assert.IsTrue(succes);

            int affectedRowsDelete = databaseConnection.ExecuteNonQuery("DELETE FROM toxication WHERE name = @name", new MySqlParameter("@name", "TestGif"));
            Assert.AreEqual(1, affectedRowsDelete);
        }

        [TestMethod]
        public void SendMessage()
        {
            bool succes = sitaApi.SendMessage(null, 1, "ditIsEenTestBericht");
            Assert.IsTrue(succes);

            int affectedRowsDelete = databaseConnection.ExecuteNonQuery("DELETE FROM message WHERE description = @description", new MySqlParameter("@description", "ditIsEenTestBericht"));
            Assert.AreEqual(1, affectedRowsDelete);
        }

        [TestMethod]
        public void SendMessageWithMedia()
        {
            bool succes = sitaApi.SendMessageWithMedia(null, 1, "ditIsEenTweedeTestBericht", 1);
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
        public void GetMediaOfIncidentTest()
        {
            object[] dataSet = sitaApi.GetMediaOfIncident("", 1, 1, 0);

            bool result = false;
            if (dataSet.Length >= 1)
            {
                result = true;
            }

            Assert.IsNotNull(dataSet);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GetMediaOfIncidentFTest()
        {
            object[] dataSet = sitaApi.GetMediaOfIncidentF("", 1, "", 1, 0);

            bool result = false;
            if (dataSet.Length >= 1)
            {
                result = true;
            }

            Assert.IsNotNull(dataSet);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GetMediaTest()
        {
            object[] dataSet = sitaApi.GetMedia("", 1, 0);

            bool result = false;
            if (dataSet.Length >= 1)
            {
                result = true;
            }

            Assert.IsNotNull(dataSet);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GetMediaFTest()
        {
            object[] dataSet = sitaApi.GetMediaF("", "", 1, 0);

            bool result = false;
            if (dataSet.Length >= 1)
            {
                result = true;
            }

            Assert.IsNotNull(dataSet);
            Assert.IsTrue(result);
        }
    }
}
