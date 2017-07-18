using System.Runtime.Serialization;

namespace obu1service.DataContracts
{
    [DataContract(Name = "Emissions")]
    public class Emissions
    {
        private double COEmissions;
        private double CO2Emissions;
        private double HCEmissions;
        private double noiseEmissions;
        private double NOxEmissions;
        private double PMxEmissions;

        [DataMember]
        public double CO
        {
            get { return COEmissions; }
            set { COEmissions = value; }
        }

        [DataMember]
        public double CO2
        {
            get { return CO2Emissions; }
            set { CO2Emissions = value; }
        }

        [DataMember]
        public double HC
        {
            get { return HCEmissions; }
            set { HCEmissions = value; }
        }

        [DataMember]
        public double Noise
        {
            get { return noiseEmissions; }
            set { noiseEmissions = value; }
        }

        [DataMember]
        public double NOx
        {
            get { return NOxEmissions; }
            set { NOxEmissions = value; }
        }

        [DataMember]
        public double PMx
        {
            get { return PMxEmissions; }
            set { PMxEmissions = value; }
        }
    }
}