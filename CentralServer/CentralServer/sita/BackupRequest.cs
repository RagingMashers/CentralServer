using System;

namespace CentralServer.sita {
	public class BackupRequest {
		private readonly int id;
		public int Id {
			get {
				return id;
			}
		}
		private DateTime dateAndTime;
		public DateTime DateAndTime {
			get {
				return dateAndTime;
			}
			set {
				dateAndTime = value;
			}
		}
		private RequestFullFilled fullFilled;
		public RequestFullFilled FullFilled {
			get {
				return fullFilled;
			}
			set {
				fullFilled = value;
			}
		}
		private double longtitude;
		public double Longtitude {
			get {
				return longtitude;
			}
			set {
				longtitude = value;
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
		private String description;
		public String Description {
			get {
				return description;
			}
			set {
				description = value;
			}
		}
		private ServiceType requestedService;
		public ServiceType RequestedService {
			get {
				return requestedService;
			}
			set {
				requestedService = value;
			}
		}

		private Team team_assigned2;
		private ServiceType serviceType2;

	}

}
