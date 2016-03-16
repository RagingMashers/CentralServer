using System;
namespace CentralServer.sita {
	public class Media {
		private readonly int id;
		public int Id {
			get {
				return id;
			}
		}
		private byte[] content;
		public byte[] Content {
			get {
				return content;
			}
			set {
				content = value;
			}
		}
		private String mimeType;
		public String MimeType {
			get {
				return mimeType;
			}
			set {
				mimeType = value;
			}
		}
		private DateTime date;
		public DateTime Date {
			get {
				return date;
			}
			set {
				date = value;
			}
		}
		private String source;
		public String Source {
			get {
				return source;
			}
			set {
				source = value;
			}
		}
		private MediaAccepted accepted;
		public MediaAccepted Accepted {
			get {
				return accepted;
			}
			set {
				accepted = value;
			}
		}
		private String sugestion;
		public String Sugestion {
			get {
				return sugestion;
			}
			set {
				sugestion = value;
			}
		}
		private Importance importance;
		public Importance Importance {
			get {
				return importance;
			}
			set {
				importance = value;
			}
		}

		private Incident media;
		private Category in_Category2;
		private Importance importance2;

	}

}
