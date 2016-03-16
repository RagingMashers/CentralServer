using System;
namespace CentralServer.sita {
	public class Resource {
		private readonly int id;
		public int Id {
			get {
				return id;
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

		private ResourseType type_of_resource;

	}

}
