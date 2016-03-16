using System;
namespace CentralServer.sita {
	public class ResourseType {
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
		private String description;
		public String Description {
			get {
				return description;
			}
			set {
				description = value;
			}
		}


	}

}
