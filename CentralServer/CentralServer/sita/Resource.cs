using System;
using System.Collections.Generic;

namespace CentralServer.sita {
	public class Resource {
        #region fields and properties
        private readonly int id;
		public int Id
        {
			get {return id;}
		}
		private double longitude;
		public double Longitude
        {
			get {return longitude;}
			set {longitude = value;}
		}
		private double latitude;
		public double Latitude
        {
			get {return latitude;}
			set {latitude = value;}
		}
		private bool available;
		public bool Available
        {
			get {return available;}
			set {available = value;}
		}

		private ResourseType typeOfResource;
        public ResourseType TypeOFResource
        {
            get{return typeOfResource;}
            set{typeOfResource = value;}
        }

        private List<Team> teams;
        public IReadOnlyCollection<Team> Teams => teams.AsReadOnly();
        #endregion

        /// <summary>
        /// Create instance of Resource
        /// </summary>
        /// <param name="id">The id of the resource</param>
        /// <param name="longitude">The long of the Resource's home position</param>
        /// <param name="latitude">The lat of the Resource's home position</param>
        /// <param name="available">If the resource is available at this point in time</param>
        /// <param name="typeOfResource">What kind of resource it is</param>
        public Resource(int id, double longitude, double latitude, bool available, ResourseType typeOfResource)
        {
            this.id = id;
            this.longitude = longitude;
            this.latitude = latitude;
            this.available = available;
            this.typeOfResource = typeOfResource;
            this.teams = new List<Team>();
        }

        /// <summary>
        /// Assign this resource to a team
        /// </summary>
        /// <param name="team">The team to assign to</param>
	    public void AssigntToTeam(Team team)
	    {
	        teams.Add(team);
	    }
	}

}
