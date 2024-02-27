using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FlightRadar
{
    internal class Data
    {
        public List<Crew> CrewList;
        public List<Passenger> PassengerList;
        public List<Cargo> CargoList;
        public List<CargoPlane> CargoPlaneList;
        public List<PassengerPlane> PassengerPlaneList;
        public List<Airport> AirportList;
        public List<Flight> FlightList;

        public Data()
        {
            CrewList = new List<Crew>();
            PassengerList = new List<Passenger>();
            CargoList = new List<Cargo>();
            CargoPlaneList = new List<CargoPlane>();
            PassengerPlaneList = new List<PassengerPlane>();
            AirportList = new List<Airport>();
            FlightList = new List<Flight>();
        }

        public void WriteToJson(string fileName)
        {
            JsonSerializerOptions options = new JsonSerializerOptions { IncludeFields = true, WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(this, options);
            File.WriteAllText(fileName, jsonString);            
        }
    }
}
