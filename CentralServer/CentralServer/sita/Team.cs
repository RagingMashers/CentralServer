using System;
using System.Collections.Generic;

namespace CentralServer.sita {
    public class Team
    {
        #region fields and properties
        private readonly int id;
        public int Id
        {
            get { return id; }
            set { throw new AccessViolationException("Id can not be set!"); }
        }

        private ServiceType type;
        public ServiceType Type
        {
            get { return type; }
            set { type = value; }
        }
        private DateTime startDate;
        public DateTime StartDate
        {
            get { return startDate; }
            set { startDate = value; }
        }
        private DateTime endDate;
        public DateTime EndDate
        {
            get { return endDate; }
            set { endDate = value; }
        }
        private double longitude;
        public double Longitude
        {
            get { return longitude; }
            set { longitude = value; }
        }
        private double latitude;
        public double Latitude
        {
            get { return latitude; }
            set { latitude = value; }
        }
        private readonly List<Person> personsInTeams;
        public List<Person> PersonsInTeam
        {
            get { return personsInTeams; }
            set { throw new AccessViolationException("PersonsInTeam can not be set!"); }
        }

        private readonly List<Resource> resourcesAvailableToTeam;
        public List<Resource> ResourcesAvailableToTeam
        {
            get { return resourcesAvailableToTeam; }
            set { throw new AccessViolationException("ResourceAvailableToTeam can not be set!");}
        }

        #endregion

        public Team(int id, ServiceType type, double longitude, double latitude)
            : this(id, type, longitude, latitude, DateTime.Now, DateTime.Now.AddDays(1))
        {
        }

        /// <summary>
        /// DO NOT USE! FOR SERIALISATION ONLY!
        /// </summary>
        public Team()
        {
            
        }

        public Team(int id, ServiceType type, double longitude, double latitude, DateTime startDate, DateTime endDate)
        {
            this.id = id;
            this.type = type;
            this.startDate = startDate;
            this.endDate = endDate;
            this.longitude = longitude;
            this.latitude = latitude;
            this.personsInTeams = new List<Person>();
            this.resourcesAvailableToTeam = new List<Resource>();
        }

        /// <summary>
        /// Assign a person to this team
        /// </summary>
        /// <param name="person">The person to assign</param>
        /// <returns>if the person is correctly added</returns>
        public bool AssignPerson(Person person)
        {
            if (person == null) throw new NullReferenceException();
            if (!person.Available || personsInTeams.Contains(person)) return false; //member already in team
            person.Available = false; //reserverve the person
            personsInTeams.Add(person);
            person.AssignToTeam(this);
            return true;
        }

        /// <summary>
        /// Releive a person from this team.
        /// </summary>
        /// <param name="person">The person to releive</param>
        /// <returns>If the persons is succesfully releived</returns>
        public bool ReliefPerson(Person person)
        {
            if (person == null) throw new NullReferenceException();
            if (!personsInTeams.Contains(person)) return false; //he isn't in the team
            person.Available = true;
            personsInTeams.Remove(person);
            return true;
        }

        /// <summary>
        /// Add a resource to this team
        /// </summary>
        /// <param name="resource">The resource to add</param>
        /// <returns>If the resource is succesfully added</returns>
        public bool AddResource(Resource resource)
        {
            if (resource == null) throw new NullReferenceException();
            if (!resource.Available || resourcesAvailableToTeam.Contains(resource)) return false;
            resource.Available = false; //reserver the person
            resourcesAvailableToTeam.Add(resource);
            resource.AssigntToTeam(this);
            return true;
        }

        /// <summary>
        /// Remove a resource from a team
        /// </summary>
        /// <param name="resource">The resource to add</param>
        /// <returns>If the resource was succesfully added</returns>
        public bool RemoveResource(Resource resource)
        {
            if (resource == null) throw new NullReferenceException();
            if (!resourcesAvailableToTeam.Contains(resource)) return false;
            resource.Available = true;
            resourcesAvailableToTeam.Remove(resource);
            return true;
        }
    }

}
