using System;
using System.Collections.Generic;

namespace CentralServer.sita {
	public class BackupRequest {
        #region fields and properties
        private readonly int id;
		public int Id
        {
			get {return id;}
            set { throw new AccessViolationException("Id can not be set!");}
		}
		private DateTime dateAndTime;
		public DateTime DateAndTime
        {
			get {return dateAndTime;}
			set {dateAndTime = value;}
		}
		private RequestFullFilled fullFilled;
		public RequestFullFilled FullFilled
        {
			get {return fullFilled;}
			set {fullFilled = value;}
		}
		private double longtitude;
		public double Longtitude
        {
			get {return longtitude;}
			set {longtitude = value;}
		}
		private double latitude;
		public double Latitude
        {
			get {return latitude;}
			set {latitude = value;}
		}
		private string description;
		public string Description
        {
			get {return description;}
			set {description = value;}
		}
		private ServiceType requestedService;
		public ServiceType RequestedService
        {
			get {return requestedService;}
			set {requestedService = value;}
		}

		private Team teamAssigned;
        public Team TeamAssigned
        {
            get{return teamAssigned;}
            set{teamAssigned = value;}
        }
        #endregion

        /// <summary>
        /// DO NOT USE! FOR SERIALISATION ONLY!
        /// </summary>
	    public BackupRequest()
	    {
	        
	    }

        public BackupRequest(int id, DateTime dateAndTime, RequestFullFilled fullFilled, double longtitude, double latitude, string description, ServiceType requestedService)
        {
            this.id = id;
            this.dateAndTime = dateAndTime;
            this.fullFilled = fullFilled;
            this.longtitude = longtitude;
            this.latitude = latitude;
            this.description = description;
            this.requestedService = requestedService;
        }
    }

}
