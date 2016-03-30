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
    }
}
