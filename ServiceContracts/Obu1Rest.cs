using obu1service.DataContracts;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Web.Http;

namespace obu1service.ServiceContracts
{
    [ServiceContract (Name = "Obu1Rest", Namespace = "Obu1Rest")]
    public interface IObu1Rest
    {
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "Obu1_MaxSpeed/{vehID}")]
        MaxSpeed Obu1_GetMaxSpeed(string vehID);

        [OperationContract]
        [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "Obu1_MaxSpeed")]
        void Obu1_PutMaxSpeed([FromBody]MaxSpeed obj);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "Obu1_MaxSpeed/{vehID}")]
        void Obu1_PostMaxSpeed(string vehID, [FromBody]string address);

        [OperationContract]
        [WebInvoke(Method = "DELETE", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "Obu1_MaxSpeed/{vehID}")]
        void Obu1_DeleteMaxSpeed(string vehID, [FromBody]string address);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "Obu1_AverageSpeed/{vehID}")]
        AverageSpeed Obu1_GetAverageSpeed(string vehID);

        [OperationContract]
        [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "Obu1_AverageSpeed")]
        void Obu1_PutAverageSpeed([FromBody]AverageSpeed obj);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "Obu1_AverageSpeed/{vehID}")]
        void Obu1_PostAverageSpeed(string vehID, [FromBody]string address);

        [OperationContract]
        [WebInvoke(Method = "DELETE", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "Obu1_AverageSpeed/{vehID}")]
        void Obu1_DeleteAverageSpeed(string vehID, [FromBody]string address);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "Obu1_ActiveVehicles")]
        string[] Obu1_ActiveVehicles([FromBody]string address);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "Obu1_ActiveVehicles/{vehID}")]
        bool Obu1_IsVehicleActive(string vehID, [FromBody]string address);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "Obu1_CurrentSpeed/{vehID}")]
        double OBU1_GetVehicleCurrentSpeed(string vehID, [FromBody]string address);

        [OperationContract]
        [WebInvoke(Method = "PUT", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "Obu1_CurrentSpeed/{vehID}")]
        void OBU1_SetVehicleCurrentSpeed(string vehID, [FromBody]string address, [FromBody]double speed);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "Obu1_Acceleration/{vehID}")]
        double OBU1_GetVehicleAcceleration(string vehID, [FromBody]string address);

        [OperationContract]
        [WebInvoke(Method = "PUT", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "Obu1_Acceleration/{vehID}")]
        void OBU1_SetVehicleAcceleration(string vehID, [FromBody]string address, [FromBody]double speed);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "Obu1_SafetyDistance/{vehID}")]
        double OBU1_GetVehicleSafetyDistance(string vehID, [FromBody]string address);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "Obu1_Position/{vehID}")]
        Position OBU1_GetVehiclePosition(string vehID, [FromBody]string address);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "Obu1_Emissions/{vehID}")]
        Emissions OBU1_GetVehicleEmissions(string vehID, [FromBody]string address);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "Obu1_Type/{vehID}")]
        string OBU1_GetVehicleType(string vehID, [FromBody]string address);
    }
}
