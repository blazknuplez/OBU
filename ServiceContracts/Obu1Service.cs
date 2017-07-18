using obu1service.DataContracts;
using System.ServiceModel;

namespace obu1serviceV2.ServiceContracts
{
    [ServiceContract(Name = "Obu1Service", Namespace = "Obu1ServiceV1")]
    public interface IObuService
    {
        [OperationContract(Name = "OBU1_GetVehicleType")]
        string OBU1_GetVehicleType(string address, string vehID);

        [OperationContract(Name = "OBU1_GetVehicleInformation")]
        VehicleInformation OBU1_GetVehicleInformation(string address, string vehID);

        [OperationContract(Name = "OBU1_GetVehicleEmissions")]
        Emissions OBU1_GetVehicleEmissions(string address, string vehID);

        [OperationContract(Name = "OBU1_GetVehicleRoute")]
        string[] OBU1_GetVehicleRoute(string address, string vehID);

        [OperationContract(Name = "OBU1_SetVehicleRoute")]
        void OBU1_SetVehicleRoute(string address, string vehID, string APIkey, string[] vehRoute);

        [OperationContract(Name = "OBU1_GetVehicleOccupancy")]
        int OBU1_GetVehicleOccupancy(string address, string vehID);

        [OperationContract(Name = "OBU1_GetVehicleCurrentSpeed")]
        double OBU1_GetVehicleCurrentSpeed(string address, string vehID);

        [OperationContract(Name = "OBU1_SetVehicleCurrentSpeed")]
        void OBU1_SetVehicleCurrentSpeed(string address, string vehID, string APIkey, double velocity);

        [OperationContract(Name = "OBU1_GetVehicleAcceleration")]
        double OBU1_GetVehicleAcceleration(string address, string vehID);

        [OperationContract(Name = "OBU1_SetVehicleAcceleration")]
        void OBU1_SetVehicleAcceleration(string address, string vehID, string APIkey, double acceleration);

        [OperationContract(Name = "OBU1_GetVehicleDeceleration")]
        double OBU1_GetVehicleDeceleration(string address, string vehID);

        [OperationContract(Name = "OBU1_SetVehicleDeceleration")]
        void OBU1_SetVehicleDeceleration(string address, string vehID, string APIkey, double deceleration);

        [OperationContract(Name = "OBU1_GetVehicleMaxSpeed")]
        double OBU1_GetVehicleMaxSpeed(string address, string vehID);

        [OperationContract(Name = "OBU1_GetVehicleSafetyDistance")]
        double OBU1_GetVehicleSafetyDistance(string address, string vehID);

        [OperationContract(Name = "OBU1_SetVehicleSafetyDistance")]
        void OBU1_SetVehicleSafetyDistance(string address, string vehID, double minGap);

        [OperationContract(Name = "OBU1_GetVehiclePosition")]
        Position OBU1_GetVehiclePosition(string address, string vehID);

        [OperationContract(Name = "OBU1_GetVehicleState")]
        State OBU1_GetVehicleState(string address, string vehID);

        [OperationContract(Name = "OBU1_SetVehicleLock")]
        void OBU1_SetVehicleLock(string address, string vehID, bool state);

        [OperationContract(Name = "OBU1_StartPublishingSpeed")]
        string StartPublishingSpeed(int timePeriodInSeconds, string address, string vehID);

        [OperationContract(Name = "OBU1_StartPublishingInactiveVehicles")]
        string StartPublishingInactiveVehicles(int timePeriodInSeconds, string address);

        [OperationContract(Name = "OBU1_SendMessage")]
        void OBU1_SendMessage(string address, string vehID, string message);

        [OperationContract(Name = "OBU1_SendMessageToLCD")]
        void OBU1_SendMessageToLCD(string address, string message);
       

        // TODO: Add your service operations here
    }
}
