using System.Runtime.Serialization;

namespace obu1service.DataContracts
{
    [DataContract(Name = "Position")]
    public class Position
    {
        private double x;
        private double y;

        [DataMember]
        public double X
        {
            get { return x; }
            set { x = value; }
        }

        [DataMember]
        public double Y
        {
            get { return y; }
            set { y = value; }
        }

    }
}