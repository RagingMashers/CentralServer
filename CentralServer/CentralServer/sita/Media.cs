using System;
namespace CentralServer.sita {
	public class Media {
        #region fields and properties
        private readonly int id;
		public int Id {
			get {return id;}
            set { throw new AccessViolationException("Id can not be set!");}
		}
		private byte[] content;
		public byte[] Content
        {
			get {return content;}
			set {content = value;}
		}
		private string mimeType;
		public string MimeType
        {
			get {return mimeType;}
			set {mimeType = value;}
		}
		private DateTime date;
		public DateTime Date
        {
			get {return date;}
			set {date = value;}
		}
		private string source;
		public string Source
        {
			get {return source;}
			set {source = value;}
		}
		private MediaAccepted accepted;
		public MediaAccepted Accepted
        {
			get {return accepted;}
			set {accepted = value;}
		}
		private string suggestion;
		public string Suggestion
        {
			get {return suggestion;}
			set {suggestion = value;}
		}
		private Importance importance;
		public Importance Importance
        {
			get {return importance;}
			set {importance = value;}
		}

		private int incident;
        public int Incident
        {
            get{return incident;}
            set{incident = value;}
        }

		private int category;
        public int Category
        {
            get{return category;}
            set{category = value;}
        }

        #endregion

        /// <summary>
        /// DO NOT USE! FOR SERIALISING ONLY!
        /// </summary>
	    public Media()
	    {
	        
	    }

        public Media(int id, byte[] content, string mimeType, DateTime date, string source, 
            MediaAccepted accepted, string suggestion, Importance importance, int incident,
            int category)
        {
            this.id = id;
            this.content = content;
            this.mimeType = mimeType;
            this.date = date;
            this.source = source;
            this.accepted = accepted;
            this.suggestion = suggestion;
            this.importance = importance;
            this.incident = incident;
            this.category = category;
        }

	}

}
