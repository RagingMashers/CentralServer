using System;
namespace CentralServer.sita {
	public class Toxication {
        #region fields and properties
        private readonly int id;
		public int Id
        {
			get {return id;}
		}
		private string name;
		public string Name {
			get {return name;}
			set {name = value;}
		}
		private string description;
		public string Description
        {
			get {return description;}
			set {description = value;}
		}
		private string chemicalCompound;
		public string ChemicalCompound
        {
			get {return chemicalCompound;}
			set {chemicalCompound = value;}
		}
		private int dangerLevel;
		public int DangerLevel
        {
			get {return dangerLevel;}
			set {dangerLevel = value;}
		}
		private double volatility;
		public double Volatility
        {
			get {return volatility;}
			set {volatility = value;}
		}
        #endregion

        public Toxication(int id, string name, string description, string chemicalCompound,
            int dangerLevel, double volatility)
        {
            this.id = id;
            this.name = name;
            this.description = description;
            this.chemicalCompound = chemicalCompound;
            this.dangerLevel = dangerLevel;
            this.volatility = volatility;
        }
	}

}
