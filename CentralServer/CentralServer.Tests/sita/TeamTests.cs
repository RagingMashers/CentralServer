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
    public class TeamTests
    {
        [TestMethod()]
        public void AssignReleivePersonTest()
        {
            var team = new Team(0,ServiceType.Police, 0,0);
            var person1 = new Person(0,"Test User",DateTime.Now.AddYears(-32),"Fireman",0,0);
            var person2 = new Person(0, "Test User2", DateTime.Now.AddYears(-29), "Fireman", 0, 0);

            if(!team.AssignPerson(person1))Assert.Fail("Failed to add person 1");
            if(!team.AssignPerson(person2))Assert.Fail("Failed to add person 2");

            if(!team.PersonsInTeam.Contains(person1) || !team.PersonsInTeam.Contains(person2)) Assert.Fail("Not all members where in team!");
            if(!person1.Teams.Contains(team) || !person2.Teams.Contains(team)) Assert.Fail("Team not assigned to persons");
            
            if (!team.ReleivePerson(person1)) Assert.Fail("Failed to releive person 1");
            if (!team.ReleivePerson(person2)) Assert.Fail("Failed to releive person 2");
        }

        [TestMethod()]
        public void AssignInvallidTest()
        {
            var team = new Team(0, ServiceType.Police, 0, 0);
            var person1 = new Person(0, "Test User", DateTime.Now.AddYears(-32), "Fireman", 0, 0);
            var person2 = new Person(0, "Test User2", DateTime.Now.AddYears(-29), "Fireman", 0, 0);

            if (!team.AssignPerson(person1)) Assert.Fail("Failed to add person 1");
            if (!team.AssignPerson(person2)) Assert.Fail("Failed to add person 2");
            if (team.AssignPerson(person2)) Assert.Fail("Person 2 assigned while already assigned!");
        }

        [TestMethod()]
        public void AddRemoveResourceTest()
        {
            var team = new Team(0,ServiceType.Police, 0, 0);
            var resourceType = new ResourseType(0,"Test Item","Test Item");
            var resource1 = new Resource(0,0,0,true,resourceType);
            var resource2 = new Resource(1,0,0,true,resourceType);

            if (!team.AddResource(resource1)) Assert.Fail("Couldn't add resource 1");
            if (!team.AddResource(resource2)) Assert.Fail("Couldn't add resource 2");
            if(!team.ResourcesAvailableToTeam.Contains(resource1) || !team.ResourcesAvailableToTeam.Contains(resource2))
                Assert.Fail("Not all resources where assigned to team!");

            if(!resource1.Teams.Contains(team) || !resource2.Teams.Contains(team)) Assert.Fail("Resources are not assigned to team!");
            //todo check if excualy added
            if (!team.RemoveResource(resource1)) Assert.Fail("Couldn't remove resource 1");
            if (!team.RemoveResource(resource2)) Assert.Fail("Couldn't remove resource 2");
        }

        [TestMethod()]
        public void AddRemoveResourceTestInvallid()
        {
            var team = new Team(0, ServiceType.Police, 0, 0);
            var resourceType = new ResourseType(0, "Test Item", "Test Item");
            var resource1 = new Resource(0, 0, 0, true, resourceType);
            var resource2 = new Resource(1, 0, 0, true, resourceType);
            var resource3 = new Resource(1, 0, 0, false, resourceType);

            if (!team.AddResource(resource1)) Assert.Fail("Couldn't add resource 1");
            if (!team.AddResource(resource2)) Assert.Fail("Couldn't add resource 2");
            if (team.AddResource(resource3)) Assert.Fail("Could add resource 3");
            if (team.AddResource(resource2)) Assert.Fail("Could add resource 2 twice!");
            if (!team.RemoveResource(resource1)) Assert.Fail("Couldn't remove resource 1");
            if (!team.RemoveResource(resource2)) Assert.Fail("Couldn't remove resource 2");
        }

    }
}