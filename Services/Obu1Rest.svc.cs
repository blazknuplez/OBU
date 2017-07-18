using obu1service.DataContracts;
using obu1service.ServiceContracts;
using System;
using System.Collections;
using System.ServiceModel;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Web.Http;
using System.Collections.Generic;
using System.Threading;

namespace obu1service.Services
{
    public class Obu1Rest : IObu1Rest
    {
        private static string connStr = "server=164.8.251.96;user=obu1;password=obuenota;database=obu1;";

        public MaxSpeed Obu1_GetMaxSpeed(string vehID)
        {
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                conn.Open();

                string address = "http://127.0.0.1:8080/TRAAS_WS";
                string sql = string.Format("SELECT veh_id, max_speed, address FROM maxspeed WHERE veh_id='{0}' AND address='{1}'", vehID, address);
                MySqlCommand command = new MySqlCommand(sql, conn);
                MySqlDataReader reader = command.ExecuteReader();

                reader.Read();
                
                MaxSpeed hitrost = new MaxSpeed();
                hitrost.VehId = Convert.ToString(reader[0]);
                hitrost.MaximumSpeed = Convert.ToDouble(reader[1]);
                hitrost.Address = Convert.ToString(reader[2]);

                if (hitrost.VehId == null)
                    throw new Exception("No vehicle with such ID");

                return hitrost;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                conn.Close();
            }
            return new MaxSpeed();
            //throw new Exception("No vehicle with such ID");
        }

        public void Obu1_PutMaxSpeed([FromBody]MaxSpeed obj)
        {
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                conn.Open();

                MySqlCommand command = new MySqlCommand("UPDATE maxspeed SET max_speed=@Parname1 WHERE veh_id=@Parname2 AND address=@Parname3", conn);
                command.Parameters.Add("@Parname1", MySqlDbType.Double).Value = obj.MaximumSpeed;
                command.Parameters.Add("@Parname2", MySqlDbType.String).Value = obj.VehId;
                command.Parameters.Add("@Parname3", MySqlDbType.String).Value = obj.Address;
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                conn.Close();
            }
        }

        public void Obu1_PostMaxSpeed(string vehID, [FromBody]string address)
        {
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                conn.Open();

                if (string.IsNullOrEmpty(address))
                    address = "http://127.0.0.1:8080/TRAAS_WS"; //default address

                TraasReference.ServiceImplClient client = new TraasReference.ServiceImplClient();
                client.Endpoint.Address = new EndpointAddress(address);

                try
                {
                    client.Open();
                    if (isVehicleActive(address, vehID))
                    {
                        double vehicleMaxSpeed = client.Vehicle_getMaxSpeed(vehID);

                        MySqlCommand command = new MySqlCommand("INSERT INTO maxspeed (veh_id, max_speed, address) VALUES (@Parname1, @Parname2, @Parname3)", conn);
                        command.Parameters.Add("@Parname1", MySqlDbType.String).Value = vehID;
                        command.Parameters.Add("@Parname2", MySqlDbType.Double).Value = vehicleMaxSpeed;
                        command.Parameters.Add("@Parname3", MySqlDbType.String).Value = address;
                        command.ExecuteNonQuery();
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                conn.Close();
            }
        }

        public void Obu1_DeleteMaxSpeed(string vehId, [FromBody]string address)
        {
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                conn.Open();

                if (string.IsNullOrEmpty(address))
                    address = "http://127.0.0.1:8080/TRAAS_WS"; //default address

                string sql = string.Format("DELETE FROM maxspeed WHERE veh_id='{0}' AND address='{1}'", vehId, address);
                MySqlCommand command = new MySqlCommand(sql, conn);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                conn.Close();
            }
        }

        public AverageSpeed Obu1_GetAverageSpeed(string vehID)
        {
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                conn.Open();

                string address = "http://127.0.0.1:8080/TRAAS_WS";
                string sql = string.Format("SELECT veh_id, average_speed, address FROM averagespeed WHERE veh_id='{0}' AND address='{1}'", vehID, address);
                MySqlCommand command = new MySqlCommand(sql, conn);
                MySqlDataReader reader = command.ExecuteReader();

                reader.Read();
                
                AverageSpeed hitrost = new AverageSpeed();
                hitrost.VehId = Convert.ToString(reader[0]);
                hitrost.AvgSpeed = Convert.ToDouble(reader[1]);
                hitrost.Address = Convert.ToString(reader[2]);
                
                if (hitrost.VehId == null)
                    throw new Exception("No vehicle with such ID");

                return hitrost;
                // Perform database operations
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                conn.Close();
            }
            throw new Exception("No vehicle with such ID");
        }

        public void Obu1_PutAverageSpeed([FromBody]AverageSpeed obj)
        {
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                conn.Open();

                MySqlCommand command = new MySqlCommand("UPDATE averagespeed SET average_speed=@Parname1 WHERE veh_id='@Parname2' AND address='@Parname3')", conn);
                command.Parameters.Add("@Parname1", MySqlDbType.Double).Value = obj.AvgSpeed;
                command.Parameters.Add("@Parname2", MySqlDbType.String).Value = obj.VehId;
                command.Parameters.Add("@Parname3", MySqlDbType.String).Value = obj.Address;

                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                conn.Close();
            }
        }

        public void Obu1_PostAverageSpeed(string vehID, [FromBody]string address)
        {
            MySqlConnection conn = new MySqlConnection(connStr);
            List<double> seznam = new List<double>();
            try
            {
                conn.Open();

                if (string.IsNullOrEmpty(address))
                    address = "http://127.0.0.1:8080/TRAAS_WS"; //default address

                TraasReference.ServiceImplClient client = new TraasReference.ServiceImplClient();
                client.Endpoint.Address = new EndpointAddress(address);

                try
                {
                    client.Open();
                    int counter = 0;
                    while(counter < 5)
                    {
                        if (isVehicleActive(address, vehID))
                        {
                            seznam.Add(client.Vehicle_getSpeed(vehID));
                        }
                        Thread.Sleep(3000);
                        counter++;
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

                if(seznam.Count > 0)
                {
                    double sum = 0;
                    double average;

                    foreach(double d in seznam)
                    {
                        sum += d;
                    }

                    average = sum / seznam.Count;

                    MySqlCommand command = new MySqlCommand("INSERT INTO averagespeed (veh_id, average_speed, address) VALUES (@Parname1, @Parname2, @Parname3)", conn);
                    command.Parameters.Add("@Parname1", MySqlDbType.String).Value = vehID;
                    command.Parameters.Add("@Parname2", MySqlDbType.Double).Value = average;
                    command.Parameters.Add("@Parname3", MySqlDbType.String).Value = address;
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                conn.Close();
            }
        }

        public void Obu1_DeleteAverageSpeed(string vehId, [FromBody]string address)
        {
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                conn.Open();

                if (string.IsNullOrEmpty(address))
                    address = "http://127.0.0.1:8080/TRAAS_WS"; //default address

                string sql = string.Format("DELETE FROM averagespeed WHERE veh_id='{0}' AND address='{1}'", vehId, address);
                MySqlCommand command = new MySqlCommand(sql, conn);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                conn.Close();
            }
        }

        public string[] Obu1_ActiveVehicles([FromBody]string address)
        {
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                conn.Open();

                if (string.IsNullOrEmpty(address))
                    address = "http://127.0.0.1:8080/TRAAS_WS"; //default address

                TraasReference.ServiceImplClient client = new TraasReference.ServiceImplClient();
                client.Endpoint.Address = new EndpointAddress(address);

                try
                {
                    client.Open();
                    string[] vehicleIDList = client.Vehicle_getIDList();
                    return vehicleIDList;
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                conn.Close();
            }
            throw new Exception("Napaka!");
        }

        public bool Obu1_IsVehicleActive(string vehID, [FromBody]string address)
        {
            if (string.IsNullOrEmpty(address))
                address = "http://127.0.0.1:8080/TRAAS_WS"; //default address
            return isVehicleActive(address, vehID);
        }

        public double OBU1_GetVehicleCurrentSpeed(string vehID, [FromBody]string address)
        {
            if (string.IsNullOrEmpty(address))
                address = "http://127.0.0.1:8080/TRAAS_WS"; //default address

            TraasReference.ServiceImplClient client = new TraasReference.ServiceImplClient();
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

        public void OBU1_SetVehicleCurrentSpeed(string vehID, [FromBody]string address, [FromBody]double speed)
        {
            if (string.IsNullOrEmpty(address))
                address = "http://127.0.0.1:8080/TRAAS_WS"; //default address

            TraasReference.ServiceImplClient client = new TraasReference.ServiceImplClient();
            client.Endpoint.Address = new EndpointAddress(address);

            try
            {
                if (speed <= 0)
                    throw new FaultException("Hitrost more biti pozitivna!");

                client.Open();
                if (isVehicleActive(address, vehID))
                {
                    client.Vehicle_setMaxSpeed(vehID, speed);
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

        public double OBU1_GetVehicleAcceleration(string vehID, [FromBody]string address)
        {
            if (string.IsNullOrEmpty(address))
                address = "http://127.0.0.1:8080/TRAAS_WS"; //default address

            TraasReference.ServiceImplClient client = new TraasReference.ServiceImplClient();
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

        public void OBU1_SetVehicleAcceleration(string vehID, [FromBody]string address, [FromBody]double acceleration)
        {
            if (string.IsNullOrEmpty(address))
                address = "http://127.0.0.1:8080/TRAAS_WS"; //default address

            TraasReference.ServiceImplClient client = new TraasReference.ServiceImplClient();
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

        public double OBU1_GetVehicleSafetyDistance(string vehID, [FromBody]string address)
        {
            if (string.IsNullOrEmpty(address))
                address = "http://127.0.0.1:8080/TRAAS_WS"; //default address

            TraasReference.ServiceImplClient client = new TraasReference.ServiceImplClient();
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

        public Position OBU1_GetVehiclePosition(string vehID, [FromBody]string address)
        {
            if (string.IsNullOrEmpty(address))
                address = "http://127.0.0.1:8080/TRAAS_WS"; //default address

            TraasReference.ServiceImplClient client = new TraasReference.ServiceImplClient();
            client.Endpoint.Address = new EndpointAddress(address);

            try
            {
                client.Open();
                if (isVehicleActive(address, vehID))
                {
                    TraasReference.sumoPosition2D vehiclePosition = client.Vehicle_getPosition(vehID);
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

        public Emissions OBU1_GetVehicleEmissions(string vehID, [FromBody]string address)
        {
            if (string.IsNullOrEmpty(address))
                address = "http://127.0.0.1:8080/TRAAS_WS"; //default address

            TraasReference.ServiceImplClient client = new TraasReference.ServiceImplClient();
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

        public string OBU1_GetVehicleType(string vehID, [FromBody]string address)
        {
            if (string.IsNullOrEmpty(address))
                address = "http://127.0.0.1:8080/TRAAS_WS"; //default address

            TraasReference.ServiceImplClient client = new TraasReference.ServiceImplClient();
            client.Endpoint.Address = new EndpointAddress(address);

            try
            {
                client.Open();
                if (isVehicleActive(address, vehID))
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

        private bool isVehicleActive(string address, string vehID)
        {
            if (string.IsNullOrEmpty(address))
                address = "http://127.0.0.1:8080/TRAAS_WS"; //default address

            TraasReference.ServiceImplClient client = new TraasReference.ServiceImplClient();
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
    }
}
