using System;
using CentralServer.sita;
using System.Collections.Generic;

namespace CentralServer.sita {
	public class Incident {
		private readonly int id;
		public int Id {
			get {
				return id;
			}
		}
		private int amountVictims;
		public int AmountVictims {
			get {
				return amountVictims;
			}
			set {
				amountVictims = value;
			}
		}
		private int amountWounded;
		public int AmountWounded {
			get {
				return amountWounded;
			}
			set {
				amountWounded = value;
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
		private int dangerlevel;
		public int Dangerlevel {
			get {
				return dangerlevel;
			}
			set {
				dangerlevel = value;
			}
		}

        private List<BackupRequest> backupRequests;
        public IReadOnlyCollection<BackupRequest> BackupRequests => backupRequests.AsReadOnly();

        private List<Media> mediaItems;
        public IReadOnlyCollection<Media> MediaItems => mediaItems.AsReadOnly();

        private List<Toxication> toxicElements;
        public IReadOnlyCollection<Toxication> ToxicElements => toxicElements.AsReadOnly();

        private List<Team> teamsOnLocation;
        public IReadOnlyCollection<Team> TeamsOnLocation => teamsOnLocation.AsReadOnly();

        /// <summary>
        /// The constructor for the incident class
        /// </summary>
        /// <param name="id">The identifier of an incident</param>
        /// <param name="amountVictims">The amount of victims involved with the incident</param>
        /// <param name="amountWounded">The amount of wounded involved with the incident</param>
        /// <param name="longitude">The X co�rdinate of the incident</param>
        /// <param name="latitude">The Y co�rdinate of the incident</param>
        /// <param name="dangerlevel">The level of danger for this incident</param>
        public Incident(int id, int amountVictims, int amountWounded, double longitude, double latitude, int dangerlevel)
        {
            this.id = id;
            this.amountVictims = amountVictims;
            this.amountWounded = amountWounded;
            this.longitude = longitude;
            this.latitude = latitude;
            this.dangerlevel = dangerlevel;
            backupRequests = new List<BackupRequest>();
            mediaItems = new List<Media>();
            toxicElements = new List<Toxication>();
            teamsOnLocation = new List<Team>();
        }

        /// <summary>
        /// Request backup for this incident
        /// </summary>
        /// <param name="request">The request that you want to send</param>
        /// <returns>Return the boolean that tells if the operation was successfull</returns>
		public bool RequestBackup(BackupRequest request) {
            //Do not allow empty requests
            if (request == null) throw new NullReferenceException();

            //If this requests exists in the list, do not add it a second time
            if (backupRequests.Contains(request)) return false;

            //When this request does not exist in the list, add the request
            backupRequests.Add(request);
            return true;
		}

        /// <summary>
        /// Attach any media item to this incident
        /// </summary>
        /// <param name="media">The media item you want to add</param>
		public void AttatchMedia(Media media) {
            //Do not allow empty media items
            if (media == null)throw new NullReferenceException();
            
            //When this media item does not exist in the list, add the media item
            if (!mediaItems.Contains(media))mediaItems.Add(media);
        }

        /// <summary>
        /// Add a toxication to this incident
        /// </summary>
        /// <param name="toxication">The toxication you need to add</param>
		public void AddToxication(Toxication toxication) {
            //Do not allow empty toxications
            if (toxication == null)throw new NullReferenceException();

            //When this toxication does not exist in the list, add the toxication
            if (!toxicElements.Contains(toxication))toxicElements.Add(toxication);
        }

        /// <summary>
        /// Remove a toxication to this incident
        /// </summary>
        /// <param name="toxication">The toxication you need to remove</param>
		public void RemoveToxication(Toxication toxication) {
            //Do not allow empty toxications
            if (toxication == null)throw new NullReferenceException();

            //When this toxication does exists in the list, remove the toxication
            if (toxicElements.Contains(toxication))toxicElements.Remove(toxication);
        }
	}

}
