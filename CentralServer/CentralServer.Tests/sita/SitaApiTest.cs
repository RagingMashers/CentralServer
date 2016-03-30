using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CentralServer.Database;
using System.Collections.Generic;
using CentralServer.sita;

namespace CentralServer.Tests.sita
{
    [TestClass]
    public class SitaApiTest
    {
        SitaApi sitaApi = new SitaApi();
        int incidentId = 18;

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
            sitaApi.AddToxication(null, "TestGif", "Testbeschrijving", "O213", 4, 1.0);

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
            bool succes = sitaApi.EditIncident("testToken", incidentId, 1, 1, 4.1, 1.0, 6, 6, "Extreem Grote brand TEST");
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
            bool succes = sitaApi.DeleteIncident("testToken", incidentId);
            Assert.IsTrue(succes);
        }
    }
}
