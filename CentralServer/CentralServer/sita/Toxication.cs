using System;
namespace CentralServer.sita {
	public class Toxication {
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
		private String chemicalCompound;
		public String ChemicalCompound {
			get {
				return chemicalCompound;
			}
			set {
				chemicalCompound = value;
			}
		}
		private int dangerLevel;
		public int DangerLevel {
			get {
				return dangerLevel;
			}
			set {
				dangerLevel = value;
			}
		}
		private double volatility;
		public double Volatility {
			get {
				return volatility;
			}
			set {
				volatility = value;
			}
		}


	}

}
