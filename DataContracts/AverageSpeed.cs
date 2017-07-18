using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace obu1service.DataContracts
{
    [DataContract(Name = "AverageSpeed")]
    public class AverageSpeed
    {
        private string vehId;
        private double speed;
        private string address;

        [DataMember]
        public string VehId
        {
            get { return vehId; }
            set { vehId = value; }
        }

        [DataMember]
        public double AvgSpeed
        {
            get { return speed; }
            set { speed = value; }
        }

        [DataMember]
        public string Address
        {
            get { return address; }
            set { address = value; }
        }
    }
}

