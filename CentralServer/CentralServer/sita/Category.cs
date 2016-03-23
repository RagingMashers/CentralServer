using System;
namespace CentralServer.sita {
	public class Category {
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
		private Category parent;
		public Category Parent
        {
			get {return parent;}
			set {parent = value;}
		}
        #endregion

        public Category(int id, string name, Category parent)
        {
            this.id = id;
            this.name = name;
            this.parent = parent;
        }
	}

}
