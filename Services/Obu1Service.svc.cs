using obu1service.DataContracts;
using obu1serviceV2.ServiceContracts;
using RabbitMQ.Client;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace obu1serviceV2.Services
{
    public class ObuService : IObuService
    {
        private static Random random = new Random();

        public string OBU1_GetVehicleType(string address, string vehID)
        {
            if (string.IsNullOrEmpty(address))
                address = "http://127.0.0.1:8080/TRAAS_WS"; //default address

            obu1service.TraasReference.ServiceImplClient client = new obu1service.TraasReference.ServiceImplClient();
            client.Endpoint.Address = new EndpointAddress(address);

            try
            {
                client.Open();
                if(isVehicleActive(address, vehID))
                {
                    string vehicleTypeID = client.Vehicle_getTypeID(vehID);
                    return vehicleTypeID;
                }
                throw new FaultException("Vehicle with that ID is not in simulation.");
            }
            catch (FaultException e)
            {
                client.Abort();
                throw new FaultException(e.Message);
            }
            catch (Exception e)
            {
                throw new FaultException(e.InnerException.ToString());
            }
            finally
            {
                client.Close();
            }
        }

        public VehicleInformation OBU1_GetVehicleInformation(string address, string vehID)
        {
            if (string.IsNullOrEmpty(address))
                address = "http://127.0.0.1:8080/TRAAS_WS"; //default address

            obu1service.TraasReference.ServiceImplClient client = new obu1service.TraasReference.ServiceImplClient();
            client.Endpoint.Address = new EndpointAddress(address);

            try
            {
                client.Open();
                if(isVehicleActive(address, vehID))
                {
                    VehicleInformation vehicleInfo = new VehicleInformation();

                    vehicleInfo.Length = client.Vehicle_getLength(vehID);
                    vehicleInfo.Width = client.Vehicle_getWidth(vehID);
                    vehicleInfo.EmissionClass = client.Vehicle_getEmissionClass(vehID);
                    vehicleInfo.ElectricityConsumption = client.Vehicle_getElectricityConsumption(vehID);
                    vehicleInfo.FuelConsumption = client.Vehicle_getFuelConsumption(vehID);

                    return vehicleInfo;
                }
                throw new FaultException("Vehicle with that ID is not in simulation.");
            }
            catch (FaultException e)
            {
                client.Abort();
                throw new FaultException(e.Message);
            }
            catch (Exception e)
            {
                throw new FaultException(e.InnerException.ToString());
            }
            finally
            {
                client.Close();
            }
        }

        public Emissions OBU1_GetVehicleEmissions(string address, string vehID)
        {
            if (string.IsNullOrEmpty(address))
                address = "http://127.0.0.1:8080/TRAAS_WS"; //default address

            obu1service.TraasReference.ServiceImplClient client = new obu1service.TraasReference.ServiceImplClient();
            client.Endpoint.Address = new EndpointAddress(address);

            try
            {
                client.Open();
                if (isVehicleActive(address, vehID))
                {
                    Emissions emissions = new Emissions();

                    emissions.CO = client.Vehicle_getCOEmission(vehID);
                    emissions.CO2 = client.Vehicle_getCO2Emission(vehID);
                    emissions.HC = client.Vehicle_getHCEmission(vehID);
                    emissions.Noise = client.Vehicle_getNoiseEmission(vehID);
                    emissions.NOx = client.Vehicle_getNOxEmission(vehID);
                    emissions.PMx = client.Vehicle_getPMxEmission(vehID);

                    return emissions;
                }
                throw new FaultException("Vehicle with that ID is not in simulation.");
            }
            catch (FaultException e)
            {
                client.Abort();
                throw new FaultException(e.Message);
            }
            catch (Exception e)
            {
                throw new FaultException(e.InnerException.ToString());
            }
            finally
            {
                client.Close();
            }
        }

        public string[] OBU1_GetVehicleRoute(string address, string vehID)
        {
            if (string.IsNullOrEmpty(address))
                address = "http://127.0.0.1:8080/TRAAS_WS"; //default address

            obu1service.TraasReference.ServiceImplClient client = new obu1service.TraasReference.ServiceImplClient();
            client.Endpoint.Address = new EndpointAddress(address);

            try
            {
                client.Open();
                if (isVehicleActive(address, vehID))
                {
                    string[] vehicleRoute = client.Vehicle_getRoute(vehID);
                    return vehicleRoute;
                }
                throw new FaultException("Vehicle with that ID is not in simulation.");
            }
            catch (FaultException e)
            {
                client.Abort();
                throw new FaultException(e.Message);
            }
            catch (Exception e)
            {
                throw new FaultException(e.InnerException.ToString());
            }
            finally
            {
                client.Close();
            }
        }

        public void OBU1_SetVehicleRoute(string address, string vehID, string APIkey, string[] vehRoute)
        {
            if (string.IsNullOrEmpty(address))
                address = "http://127.0.0.1:8080/TRAAS_WS"; //default address

            obu1service.TraasReference.ServiceImplClient client = new obu1service.TraasReference.ServiceImplClient();
            client.Endpoint.Address = new EndpointAddress(address);

            try
            {
                client.Open();
                if (isVehicleActive(address, vehID))
                {
                    client.Vehicle_setRoute(vehID, vehRoute);
                }
                throw new FaultException("Vehicle with that ID is not in simulation.");
            }
            catch (FaultException e)
            {
                client.Abort();
                throw new FaultException(e.Message);
            }
            catch (Exception e)
            {
                throw new FaultException(e.InnerException.ToString());
            }
            finally
            {
                client.Close();
            }
        }

        public int OBU1_GetVehicleOccupancy(string address, string vehID)
        {
            if (string.IsNullOrEmpty(address))
                address = "http://127.0.0.1:8080/TRAAS_WS"; //default address

            obu1service.TraasReference.ServiceImplClient client = new obu1service.TraasReference.ServiceImplClient();
            client.Endpoint.Address = new EndpointAddress(address);

            try
            {
                client.Open();
                if (isVehicleActive(address, vehID))
                {
                    int vehicleOccupancy = client.Vehicle_getPersonNumber(vehID);
                    return vehicleOccupancy;
                }
                throw new FaultException("Vehicle with that ID is not in simulation.");
            }
            catch (FaultException e)
            {
                client.Abort();
                throw new FaultException(e.Message);
            }
            catch (Exception e)
            {
                throw new FaultException(e.InnerException.ToString());
            }
            finally
            {
                client.Close();
            }
        }

        public double OBU1_GetVehicleCurrentSpeed(string address, string vehID)
        {
            if (string.IsNullOrEmpty(address))
                address = "http://127.0.0.1:8080/TRAAS_WS"; //default address

            obu1service.TraasReference.ServiceImplClient client = new obu1service.TraasReference.ServiceImplClient();
            client.Endpoint.Address = new EndpointAddress(address);

            try
            {
                client.Open();
                if (isVehicleActive(address, vehID))
                {
                    double vehicleSpeed = client.Vehicle_getSpeed(vehID);
                    return vehicleSpeed;
                }
                throw new FaultException("Vehicle with that ID is not in simulation.");
            }
            catch (FaultException e)
            {
                client.Abort();
                throw new FaultException(e.Message);
            }
            catch (Exception e)
            {
                throw new FaultException(e.InnerException.ToString());
            }
            finally
            {
                client.Close();
            }
        }

        public void OBU1_SetVehicleCurrentSpeed(string address, string vehID, string APIkey, double speed)
        {
            if (string.IsNullOrEmpty(address))
                address = "http://127.0.0.1:8080/TRAAS_WS"; //default address

            obu1service.TraasReference.ServiceImplClient client = new obu1service.TraasReference.ServiceImplClient();
            client.Endpoint.Address = new EndpointAddress(address);

            try
            {
                if (speed <= 0)
                    throw new FaultException("Hitrost more biti pozitivna!");

                client.Open();
                if (isVehicleActive(address, vehID))
                {
                    client.Vehicle_setMaxSpeed(vehID, speed);
                    client.Vehicle_setSpeedFactor(vehID, 16);
                    client.Vehicle_setSpeed(vehID, speed);
                }
            }
            catch (FaultException e)
            {
                client.Abort();
                throw new FaultException(e.Message);
            }
            catch (Exception e)
            {
                throw new FaultException(e.InnerException.ToString());
            }
            finally
            {
                client.Close();
            }
        }

        public double OBU1_GetVehicleAcceleration(string address, string vehID)
        {
            if (string.IsNullOrEmpty(address))
                address = "http://127.0.0.1:8080/TRAAS_WS"; //default address

            obu1service.TraasReference.ServiceImplClient client = new obu1service.TraasReference.ServiceImplClient();
            client.Endpoint.Address = new EndpointAddress(address);

            try
            {
                client.Open();
                if (isVehicleActive(address, vehID))
                {
                    double vehicleAcceleration = client.Vehicle_getAccel(vehID);
                    return vehicleAcceleration;
                }
                throw new FaultException("Vehicle with that ID is not in simulation.");
            }
            catch (FaultException e)
            {
                client.Abort();
                throw new FaultException(e.Message);
            }
            catch (Exception e)
            {
                throw new FaultException(e.InnerException.ToString());
            }
            finally
            {
                client.Close();
            }
        }

        public void OBU1_SetVehicleAcceleration(string address, string vehID, string APIkey, double acceleration)
        {
            if (string.IsNullOrEmpty(address))
                address = "http://127.0.0.1:8080/TRAAS_WS"; //default address

            obu1service.TraasReference.ServiceImplClient client = new obu1service.TraasReference.ServiceImplClient();
            client.Endpoint.Address = new EndpointAddress(address);

            try
            {
                client.Open();
                if (isVehicleActive(address, vehID))
                {
                    client.Vehicle_setAccel(vehID, acceleration);
                }
            }
            catch (FaultException e)
            {
                client.Abort();
                throw new FaultException(e.Message);
            }
            catch (Exception e)
            {
                throw new FaultException(e.InnerException.ToString());
            }
            finally
            {
                client.Close();
            }
        }

        public double OBU1_GetVehicleDeceleration(string address, string vehID)
        {
            if (string.IsNullOrEmpty(address))
                address = "http://127.0.0.1:8080/TRAAS_WS"; //default address

            obu1service.TraasReference.ServiceImplClient client = new obu1service.TraasReference.ServiceImplClient();
            client.Endpoint.Address = new EndpointAddress(address);

            try
            {
                client.Open();
                if (isVehicleActive(address, vehID))
                {
                    double vehicleDeceleration = client.Vehicle_getDecel(vehID);
                    return vehicleDeceleration;
                }
                throw new FaultException("Vehicle with that ID is not in simulation.");
            }
            catch (FaultException e)
            {
                client.Abort();
                throw new FaultException(e.Message);
            }
            catch (Exception e)
            {
                throw new FaultException(e.InnerException.ToString());
            }
            finally
            {
                client.Close();
            }
        }

        public void OBU1_SetVehicleDeceleration(string address, string vehID, string APIkey, double deceleration)
        {
            if (string.IsNullOrEmpty(address))
                address = "http://127.0.0.1:8080/TRAAS_WS"; //default address

            obu1service.TraasReference.ServiceImplClient client = new obu1service.TraasReference.ServiceImplClient();
            client.Endpoint.Address = new EndpointAddress(address);

            try
            {
                client.Open();
                if (isVehicleActive(address, vehID))
                {
                    client.Vehicle_setDecel(vehID, deceleration);
                }
            }
            catch (FaultException e)
            {
                client.Abort();
                throw new FaultException(e.Message);
            }
            catch (Exception e)
            {
                throw new FaultException(e.InnerException.ToString());
            }
            finally
            {
                client.Close();
            }
        }

        public double OBU1_GetVehicleMaxSpeed(string address, string vehID)
        {
            if (string.IsNullOrEmpty(address))
                address = "http://127.0.0.1:8080/TRAAS_WS"; //default address

            obu1service.TraasReference.ServiceImplClient client = new obu1service.TraasReference.ServiceImplClient();
            client.Endpoint.Address = new EndpointAddress(address);

            try
            {
                client.Open();
                if (isVehicleActive(address, vehID))
                {
                    double vehicleMaxSpeed = client.Vehicle_getMaxSpeed(vehID);
                    return vehicleMaxSpeed;
                }
                throw new FaultException("Vehicle with that ID is not in simulation.");
            }
            catch (FaultException e)
            {
                client.Abort();
                throw new FaultException(e.Message);
            }
            catch (Exception e)
            {
                throw new FaultException(e.InnerException.ToString());
            }
            finally
            {
                client.Close();
            }
        }

        public double OBU1_GetVehicleSafetyDistance(string address, string vehID)
        {
            if (string.IsNullOrEmpty(address))
                address = "http://127.0.0.1:8080/TRAAS_WS"; //default address

            obu1service.TraasReference.ServiceImplClient client = new obu1service.TraasReference.ServiceImplClient();
            client.Endpoint.Address = new EndpointAddress(address);

            try
            {
                client.Open();
                if (isVehicleActive(address, vehID))
                {
                    double vehicleSafetyDistance = client.Vehicle_getMinGap(vehID);
                    return vehicleSafetyDistance;
                }
                throw new FaultException("Vehicle with that ID is not in simulation.");
            }
            catch (FaultException e)
            {
                client.Abort();
                throw new FaultException(e.Message);
            }
            catch (Exception e)
            {
                throw new FaultException(e.InnerException.ToString());
            }
            finally
            {
                client.Close();
            }

        }

        public void OBU1_SetVehicleSafetyDistance(string address, string vehID, double minGap)
        {
            if (string.IsNullOrEmpty(address))
                address = "http://127.0.0.1:8080/TRAAS_WS"; //default address

            obu1service.TraasReference.ServiceImplClient client = new obu1service.TraasReference.ServiceImplClient();
            client.Endpoint.Address = new EndpointAddress(address);

            try
            {
                client.Open();
                if (isVehicleActive(address, vehID))
                {
                    client.Vehicle_setMinGap(vehID, minGap);
                }
            }
            catch (FaultException e)
            {
                client.Abort();
                throw new FaultException(e.Message);
            }
            catch (Exception e)
            {
                throw new FaultException(e.InnerException.ToString());
            }
            finally
            {
                client.Close();
            }
        }

        public Position OBU1_GetVehiclePosition(string address, string vehID)
        {
            if (string.IsNullOrEmpty(address))
                address = "http://127.0.0.1:8080/TRAAS_WS"; //default address

            obu1service.TraasReference.ServiceImplClient client = new obu1service.TraasReference.ServiceImplClient();
            client.Endpoint.Address = new EndpointAddress(address);

            try
            {
                client.Open();
                if (isVehicleActive(address, vehID))
                {
                    obu1service.TraasReference.sumoPosition2D vehiclePosition = client.Vehicle_getPosition(vehID);
                    Position position = new Position();
                    position.X = vehiclePosition.x;
                    position.Y = vehiclePosition.y;
                    return position;
                }
                throw new FaultException("Vehicle with that ID is not in simulation.");
            }
            catch (FaultException e)
            {
                client.Abort();
                throw new FaultException(e.Message);
            }
            catch (Exception e)
            {
                throw new FaultException(e.InnerException.ToString());
            }
            finally
            {
                client.Close();
            }
        }

        public State OBU1_GetVehicleState(string address, string vehID)
        {
            if (string.IsNullOrEmpty(address))
                address = "http://127.0.0.1:8080/TRAAS_WS"; //default address

            obu1service.TraasReference.ServiceImplClient client = new obu1service.TraasReference.ServiceImplClient();
            client.Endpoint.Address = new EndpointAddress(address);

            try
            {
                client.Open();
                if (isVehicleActive(address, vehID))
                {
                    State state = new State();

                    int signals = client.Vehicle_getSignals(vehID);
                    BitArray bits = new BitArray(signals);
                    bool[] myBools = new bool[32];
                    var array = Convert.ToString(signals, 2).Select(s => s.Equals('1')).ToArray().Reverse().ToArray();
                    Array.Copy(array, myBools, array.Count());

                    if (myBools[9] || myBools[10])
                        state.Locked = false;
                    else
                        state.Locked = true;

                    double velocity = client.Vehicle_getSpeed(vehID);
                    if (velocity > 0)
                        state.EngineOn = true;
                    else
                        state.EngineOn = false;

                    return state;
                }
                throw new FaultException("Vehicle with that ID is not in simulation.");
            }
            catch (FaultException e)
            {
                client.Abort();
                throw new FaultException(e.Message);
            }
            catch (Exception e)
            {
                throw new FaultException(e.InnerException.ToString());
            }
            finally
            {
                client.Close();
            }
        }
        
        public void OBU1_SetVehicleLock(string address, string vehID, bool state)
        {
            if (string.IsNullOrEmpty(address))
                address = "http://127.0.0.1:8080/TRAAS_WS"; //default address

            obu1service.TraasReference.ServiceImplClient client = new obu1service.TraasReference.ServiceImplClient();
            client.Endpoint.Address = new EndpointAddress(address);

            try
            {
                client.Open();
                if (isVehicleActive(address, vehID))
                {
                    int signals = client.Vehicle_getSignals(vehID);
                    bool[] myBools = new bool[32];
                    var array = Convert.ToString(signals, 2).Select(s => s.Equals('1')).ToArray().Reverse().ToArray();
                    Array.Copy(array, myBools, array.Count());

                    myBools[3] = true;
                    myBools[9] = !state;
                    myBools[10] = !state;

                    client.Vehicle_setSignals(vehID, BoolArrayToInt(myBools));
                }
            }
            catch (FaultException e)
            {
                client.Abort();
                throw new FaultException(e.Message);
            }
            catch (Exception e)
            {
                throw new FaultException(e.InnerException.ToString());
            }
            finally
            {
                client.Close();
            }
        }

        public string StartPublishingSpeed(int timePeriodInSeconds, string address, string vehID)
        {
            string random = RandomString(16);
            string queueName = "OBU1_VehicleSpeed_" + vehID + "_" + random;
            Task.Run(() => publishSpeedAsync(queueName, timePeriodInSeconds, address, vehID));
            return queueName;
        }

        private async Task publishSpeedAsync(string queueName, int timePeriodInSeconds, string address, string vehID)
        {
            if (string.IsNullOrEmpty(address))
                address = "http://127.0.0.1:8080/TRAAS_WS"; //default address

            obu1service.TraasReference.ServiceImplClient client = new obu1service.TraasReference.ServiceImplClient();

            client.Endpoint.Address = new EndpointAddress(address);

            ConnectionFactory factory = new ConnectionFactory();
            IProtocol protocol = Protocols.AMQP_0_9_1;
            factory.VirtualHost = "/";
            factory.UserName = "soa";
            factory.Password = "soasoa";
            factory.HostName = "164.8.251.96";
            factory.Port = 5672;
            factory.Protocol = protocol;

            try
            {
                using (IConnection conn = factory.CreateConnection())
                {
                    using (IModel ch = conn.CreateModel())
                    {
                        ch.QueueDeclare(queue: queueName,
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                        Stopwatch stopwatch = new Stopwatch();
                        stopwatch.Start();

                        client.Open();
                        while (stopwatch.Elapsed.TotalSeconds < timePeriodInSeconds && isVehicleActive(address, vehID))
                        {

                            double speed = client.Vehicle_getSpeed(vehID);

                            var body = Encoding.UTF8.GetBytes(speed.ToString());

                            ch.BasicPublish(exchange: "",
                                                 routingKey: queueName,
                                                 basicProperties: null,
                                                 body: body);

                            System.Threading.Thread.Sleep(5000);
                            //client.Close();
                        }

                        client.Close();
                    }
                }

            }
            catch (FaultException e)
            {
                client.Abort();

            }
            catch (Exception e)
            {
                client.Abort();
            }
        }

        public string StartPublishingInactiveVehicles(int timePeriodInSeconds, string address)
        {
            string random = RandomString(16);
            string queueName = "OBU1_InactiveVehicles_" + random;
            Task.Run(() => publishInactiveVehiclesAsync(queueName, timePeriodInSeconds, address));
            return queueName;
        }

        private async Task publishInactiveVehiclesAsync(string queueName, int timePeriodInSeconds, string address)
        {
            if (string.IsNullOrEmpty(address))
                address = "http://127.0.0.1:8080/TRAAS_WS"; //default address

            obu1service.TraasReference.ServiceImplClient client = new obu1service.TraasReference.ServiceImplClient();

            client.Endpoint.Address = new EndpointAddress(address);

            ConnectionFactory factory = new ConnectionFactory();
            IProtocol protocol = Protocols.AMQP_0_9_1;
            factory.VirtualHost = "/";
            factory.UserName = "soa";
            factory.Password = "soasoa";
            factory.HostName = "164.8.251.96";
            factory.Port = 5672;
            factory.Protocol = protocol;

            try
            {
                using (IConnection conn = factory.CreateConnection())
                {
                    using (IModel ch = conn.CreateModel())
                    {
                        ch.QueueDeclare(queue: queueName,
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                        Stopwatch stopwatch = new Stopwatch();
                        stopwatch.Start();
                        Dictionary<string, bool> seznamVozil = new Dictionary<string, bool>();

                        client.Open();
                        while (stopwatch.Elapsed.TotalSeconds < timePeriodInSeconds)
                        {

                            string[] activeVehicles = client.Vehicle_getIDList();
                            foreach (string s in activeVehicles)
                            {
                                if (seznamVozil.ContainsKey(s))
                                    seznamVozil[s] = true;
                                else
                                    seznamVozil.Add(s, true);
                            }

                            List<string> inactiveVehicles = new List<string>();
                            foreach (KeyValuePair<string, bool> entry in seznamVozil)
                            {
                                if (!entry.Value)
                                    inactiveVehicles.Add(entry.Key);
                            }

                            string msg = "";

                            foreach (string s in inactiveVehicles)
                            {
                                msg += s + " ";
                                seznamVozil.Remove(s);
                            }

                            if (msg != "")
                            {
                                var body = Encoding.UTF8.GetBytes(msg);

                                ch.BasicPublish(exchange: "",
                                                     routingKey: queueName,
                                                     basicProperties: null,
                                                     body: body);
                            }

                            foreach (var key in seznamVozil.Keys.ToList())
                            {
                                seznamVozil[key] = false;
                            }

                            System.Threading.Thread.Sleep(5000);
                            //client.Close();
                        }

                        client.Close();
                    }
                }

            }
            catch (FaultException e)
            {
                client.Abort();

            }
            catch (Exception e)
            {
                client.Abort();
            }
        }

        public void OBU1_SendMessage(string address, string vehID, string message)
        {
            Console.WriteLine(address + " | " + vehID + " | " + message);
        }

        public void OBU1_SendMessageToLCD(string address, string message)
        {
            sendRequest(address + "/index.php?msg=" + message);
        }

        private string sendRequest(string url)
        {
            try
            {
                using (WebClient client = new WebClient())
                {
                    return client.DownloadString(new Uri(url));
                }
            }
            catch (WebException ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        private bool isVehicleActive(string address, string vehID)
        {
            if (string.IsNullOrEmpty(address))
                address = "http://127.0.0.1:8080/TRAAS_WS"; //default address

            obu1service.TraasReference.ServiceImplClient client = new obu1service.TraasReference.ServiceImplClient();
            client.Endpoint.Address = new EndpointAddress(address);

            try
            {
                client.Open();
                string[] vehicleIDList = client.Vehicle_getIDList();
                foreach (string searchedID in vehicleIDList)
                {
                    if (searchedID == vehID)
                        return true;
                }
                return false;
            }
            catch (FaultException e)
            {
                client.Abort();
                throw new FaultException(e.Message);
            }
            catch (Exception e)
            {
                throw new FaultException(e.InnerException.ToString());
            }
            finally
            {
                client.Close();
            }
        }

        private int BoolArrayToInt(bool[] bits)
        {
            if (bits.Length > 32) throw new ArgumentException("Can only fit 32 bits in a uint");

            int r = 0;
            for (int i = 0; i < bits.Length; i++)
            {
                if (bits[i]) {
                    r += (int)Math.Pow(2, i);
                }
            }
            return r;
        }

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

    }
}
