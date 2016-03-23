using Microsoft.VisualStudio.TestTools.UnitTesting;
using CentralServer.sita;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentralServer.sita.Tests
{
    [TestClass()]
    public class IncidentTests
    {
        private Incident incident;

        [TestInitialize()]
        public void Initialize() {
            incident = new Incident(1, 1, 1, 1.0, 1.0, 1,"testdescription");
        }

        [TestMethod()]
        public void IdTest()
        {
            Assert.AreEqual(1, incident.Id, "\"IdTest\" failed, ID Should be 1 but was" + incident.Id);
        }

        [TestMethod()]
        public void AmountVictimsTest()
        {
            Assert.AreEqual(1, incident.AmountVictims, "\"AmountVictimsTest\" failed, ID Should be 1 but was" + incident.AmountVictims);
        }

        [TestMethod()]
        public void AmountWoundedTest()
        {
            Assert.AreEqual(1, incident.AmountWounded, "\"AmountWoundedTest\" failed, ID Should be 1 but was" + incident.AmountWounded);
        }

        [TestMethod()]
        public void LongitudeTest()
        {
            Assert.AreEqual(1.0, incident.Longitude, "\"LongitudeTest\" failed, ID Should be 1.0 but was" + incident.Longitude);
        }

        [TestMethod()]
        public void LatitudeTest()
        {
            Assert.AreEqual(1.0, incident.Latitude, "\"LatitudeTest\" failed, ID Should be 1.0 but was" + incident.Latitude);
        }

        [TestMethod()]
        public void DangerLevelTest()
        {
            Assert.AreEqual(1, incident.Dangerlevel, "\"DangerLevelTest\" failed, ID Should be 1 but was" + incident.Dangerlevel);
        }

        [TestMethod()]
        [ExpectedException(typeof(NullReferenceException), "\"RequestBackupTestNull\" failed, it should throw a NullReferenceException ")]
        public void RequestBackupTestNull()
        {
            BackupRequest request = null;
            incident.RequestBackup(request);
        }

        [TestMethod()]
        public void RequestBackupTestGood()
        {
            BackupRequest request = new BackupRequest(1, new DateTime(2016, 3, 16), RequestFullFilled.FORMING, 1.0, 1.0, "description", ServiceType.ME);
            Assert.AreEqual(true, incident.RequestBackup(request), "\"RequestBackupTestGood\" failed, it should return true but it returned false");
        }

        [TestMethod()]
        public void RequestBackupTestDouble()
        {
            BackupRequest request = new BackupRequest(1, new DateTime(2016, 3, 16), RequestFullFilled.FORMING, 1.0, 1.0, "description", ServiceType.ME);
            incident.RequestBackup(request);
            Assert.AreEqual(false, incident.RequestBackup(request), "\"RequestBackupTestDouble\" failed, it should return false but it returned true");
        }

        [TestMethod()]
        [ExpectedException(typeof(NullReferenceException), "\"AttatchMediaTestNull\" failed, it should throw a NullReferenceException ")]
        public void AttatchMediaTestNull()
        {
            Media media = null;
            incident.AttatchMedia(media);
        }

        [TestMethod()]
        public void AttatchMediaTest()
        {
            Media media = new Media(1, new byte[10], "", new DateTime(2016, 3, 16), "", MediaAccepted.NO, "", Importance.HIGH, incident, new Category(1,"test",null));
            incident.AttatchMedia(media);
            Assert.AreEqual(1, incident.MediaItems.Count, "\"AttatchMediaTest\" failed, it should have one item while it has " + incident.MediaItems.Count);
        }

        [TestMethod()]
        [ExpectedException(typeof(NullReferenceException), "\"AddToxicationTestNull\" failed, it should throw a NullReferenceException ")]
        public void AddToxicationTestNull()
        {
            Toxication toxication = null;
            incident.AddToxication(toxication);
        }

        [TestMethod()]
        public void AddToxicationTest()
        {
            Toxication toxication = new Toxication(1, "", "", "", 1, 1.0);
            incident.AddToxication(toxication);
            Assert.AreEqual(1, incident.ToxicElements.Count, "\"AttatchMediaTest\" failed, it should have one item while it has " + incident.ToxicElements.Count);
        }

        [TestMethod()]
        [ExpectedException(typeof(NullReferenceException), "\"RemoveToxicationTestNull\" failed, it should throw a NullReferenceException ")]
        public void RemoveToxicationTestNull()
        {
            Toxication toxication = null;
            incident.RemoveToxication(toxication);
        }

        [TestMethod()]
        public void RemoveToxicationTest()
        {
            Toxication toxication = new Toxication(1, "", "", "", 1, 1.0);
            incident.AddToxication(toxication);
            incident.RemoveToxication(toxication);
            Assert.AreEqual(0, incident.ToxicElements.Count, "\"AttatchMediaTest\" failed, it should have zero items while it has " + incident.ToxicElements.Count);
        }
    }
}