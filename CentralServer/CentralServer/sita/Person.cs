using System;
namespace CentralServer.sita {
	public class Person {
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

		private System.Collections.Generic.List<Team> teams;

	}

}
