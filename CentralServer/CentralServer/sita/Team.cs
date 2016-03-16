using System;
namespace CentralServer.sita {
	public class Team {
		private readonly int id;
		public int Id {
			get {
				return id;
			}
		}
		private ServiceType type;
		public ServiceType Type {
			get {
				return type;
			}
			set {
				type = value;
			}
		}
		private DateTime startDate;
		public DateTime StartDate {
			get {
				return startDate;
			}
			set {
				startDate = value;
			}
		}
		private DateTime endDate;
		public DateTime EndDate {
			get {
				return endDate;
			}
			set {
				endDate = value;
			}
		}
		private double longitude;
		public double Longitude {
			get {
				return longitude;
			}
			set {
				longitude = value;
			}
		}
		private double latitude;
		public double Latitude {
			get {
				return latitude;
			}
			set {
				latitude = value;
			}
		}

		public bool AssignPerson(ref Person person) {
			throw new System.Exception("Not implemented");
		}
		public bool ReleivePerson(ref Person person) {
			throw new System.Exception("Not implemented");
		}
		public bool AddResource(ref Resource resource) {
			throw new System.Exception("Not implemented");
		}
		public bool RemoveResource(ref Resource resource) {
			throw new System.Exception("Not implemented");
		}

		private System.Collections.Generic.List<Incident> teams_on_location2;
		private ServiceType serviceType;

		private System.Collections.Generic.List<Person> persons_in_teams;
		private System.Collections.Generic.List<Resource> resources_available_to_team;

	}

}
