using System;
namespace CentralServer.sita {
	public class ResourseType {
        #region fields and properties
        private readonly int id;
		public int Id
        {
			get {return id;}
		}

		private string name;
		public string Name
        {
			get {return name;}
			set {name = value;}
		}

		private string description;
		public string Description
        {
			get {return description;}
			set {description = value;}
		}
        #endregion

        public ResourseType(int id, string name, string description)
        {
            this.id = id;
            this.name = name;
            this.description = description;
        }
    }

}
