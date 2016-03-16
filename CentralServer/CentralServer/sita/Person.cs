using System;
using System.Collections.Generic;

namespace CentralServer.sita {
	public class Person {
        /// <summary>
        /// Create a new instance of Person
        /// </summary>
        /// <param name="id">The persons id</param>
        /// <param name="name">The persons name</param>
        /// <param name="dayOfBirth">The birthdate of the person</param>
        /// <param name="occupation">The occupation of the person</param>
        /// <param name="homeLongitude">The long of the persons home address</param>
        /// <param name="homeLatitude">The lat of the persons address</param>
	    public Person(int id, string name, DateTime dayOfBirth, string occupation, double homeLongitude, double homeLatitude)
	    {
	        this.id = id;
	        this.name = name;
	        this.dayOfBirth = dayOfBirth;
	        this.occupation = occupation;
	        this.homeLongitude = homeLongitude;
	        this.homeLatitude = homeLatitude;
	        this.available = true;
	        this.teams = new List<Team>();
	    }

		private readonly int id;
		public int Id {
			get {
				return id;
			}
		}
		private String name;
		public String Name {
			get {
				return name;
			}
			set {
				name = value;
			}
		}
		private DateTime dayOfBirth;
		public DateTime DayOfBirth {
			get {
				return dayOfBirth;
			}
			set {
				dayOfBirth = value;
			}
		}
		private String occupation;
		public String Occupation {
			get {
				return occupation;
			}
			set {
				occupation = value;
			}
		}
		private double homeLongitude;
		public double HomeLongitude {
			get {
				return homeLongitude;
			}
			set {
				homeLongitude = value;
			}
		}
		private double homeLatitude;
		public double HomeLatitude {
			get {
				return homeLatitude;
			}
			set {
				homeLatitude = value;
			}
		}
		private bool available;
		public bool Available {
			get {
				return available;
			}
			set {
				available = value;
			}
		}

        /// <summary>
        /// Assign this person to a team
        /// </summary>
        /// <param name="team">The team to assign to</param>
	    public void AssignToTeam(Team team)
	    {
	        teams.Add(team);
	    }

		private List<Team> teams;

	    public IReadOnlyCollection<Team> Teams => teams.AsReadOnly();

	}

}
