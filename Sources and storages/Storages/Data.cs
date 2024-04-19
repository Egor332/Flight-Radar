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
        public Dictionary<UInt64, Crew> CrewDictionary;
        public Dictionary<UInt64, Passenger> PassengerDictionary;
        public Dictionary<UInt64, Cargo> CargoDictionary;
        public Dictionary<UInt64, CargoPlane> CargoPlaneDictionary;
        public Dictionary<UInt64, PassengerPlane> PassengerPlaneDictionary;
        public Dictionary<UInt64, Airport> AirportDictionary;
        public Dictionary<UInt64, Flight> FlightDictionary;

        public Data()
        {
            CrewDictionary = new Dictionary<UInt64, Crew>();
            PassengerDictionary = new Dictionary<UInt64, Passenger>();
            CargoDictionary = new Dictionary<UInt64, Cargo>();
            CargoPlaneDictionary = new Dictionary<UInt64, CargoPlane>();
            PassengerPlaneDictionary = new Dictionary<UInt64, PassengerPlane>();
            AirportDictionary = new Dictionary<UInt64, Airport>();
            FlightDictionary = new Dictionary<UInt64, Flight>();
        }

        public void WriteToJson(string fileName)
        {
            JsonSerializerOptions options = new JsonSerializerOptions { IncludeFields = true, WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(this, options);
            File.WriteAllText(fileName, jsonString);            
        }
    }
}
