using System;
using CentralServer.sita;

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
		private double longditude;
		public double Longditude {
			get {
				return longditude;
			}
			set {
				longditude = value;
			}
		}
		private double latidude;
		public double Latidude {
			get {
				return latidude;
			}
			set {
				latidude = value;
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

		public bool RequestBackup(ref BackupRequest request) {
			throw new System.Exception("Not implemented");
		}
		public void AttatchMedia(ref Media media) {
			throw new System.Exception("Not implemented");
		}
		public void AddToxication(ref Toxication toxication) {
			throw new System.Exception("Not implemented");
		}
		public void RemoveToxication(ref Toxication toxication) {
			throw new System.Exception("Not implemented");
		}

		private System.Collections.Generic.List<BackupRequest> backup_Requests;
		private System.Collections.Generic.List<Media> media2;

		private System.Collections.Generic.List<Toxication> has_toxic_elements2;

	}

}
