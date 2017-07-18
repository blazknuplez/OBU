using System.Runtime.Serialization;

namespace obu1service.DataContracts
{
    [DataContract(Name = "VehicleInformation")]
    public class VehicleInformation
    {
        private double length;
        private double width;
        private string emissionClass;
        private double electricityConsumption;
        private double fuelConsumption;

        [DataMember]
        public double Length
        {
            get { return length; }
            set { length = value; }
        }

        [DataMember]
        public double Width
        {
            get { return width; }
            set { width = value; }
        }

        [DataMember]
        public string EmissionClass
        {
            get { return emissionClass; }
            set { emissionClass = value; }
        }

        [DataMember]
        public double ElectricityConsumption
        {
            get { return electricityConsumption; }
            set { electricityConsumption = value; }
        }

        [DataMember]
        public double FuelConsumption
        {
            get { return fuelConsumption; }
            set { fuelConsumption = value; }
        }
    }
}