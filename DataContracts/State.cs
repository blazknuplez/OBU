using System.Runtime.Serialization;

namespace obu1service.DataContracts
{
    [DataContract(Name = "State")]
    public class State
    {
        private bool engineOn;
        private bool locked;

        [DataMember]
        public bool EngineOn
        {
            get { return engineOn; }
            set { engineOn = value; }
        }

        [DataMember]
        public bool Locked
        {
            get { return locked; }
            set { locked = value; }
        }

    }
}