using System;
namespace CentralServer.sita {
	public class Category {
		private readonly int id;
		public int Id {
			get {
				return id;
			}
		}
		private String name;
		public String Name {
			get {
				return name;
			}
			set {
				name = value;
			}
		}
		private Category parent;
		public Category Parent {
			get {
				return parent;
			}
			set {
				parent = value;
			}
		}


		private Category part_of_category;

	}

}
