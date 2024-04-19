using NetworkSourceSimulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightRadar
{
    internal class Generator
    {
        private static readonly Dictionary<string, Action<string[], Data>> _StringGenerators = new Dictionary<string, Action<string[], Data>>
        {
            {"C", (args, data) => GenerateCrew(args, data)},
            {"P", (args, data) => GeneratePassenger(args, data)},
            {"CA", (args, data) => GenerateCargo(args, data)},
            {"CP", (args, data) => GenerateCargoPlane(args, data)},
            {"PP", (args, data) => GeneratePassengerPlane(args, data)},
            {"AI", (args, data) => GenerateAirport(args, data)},
            {"FL", (args, data) => GenerateFlight(args, data)}
        };

        private static readonly Dictionary<string, Action<byte[], Data>> _ByteGenerators = new Dictionary<string, Action<byte[], Data>>
        {
            {"NCR", (args, data) => GenerateCrew(args, data)},
            {"NPA", (args, data) => GeneratePassenger(args, data)},
            {"NCA", (args, data) => GenerateCargo(args, data)},
            {"NCP", (args, data) => GenerateCargoPlane(args, data)},
            {"NPP", (args, data) => GeneratePassengerPlane(args, data)},
            {"NAI", (args, data) => GenerateAirport(args, data)},
            {"NFL", (args, data) => GenerateFlight(args, data)}
        };

        private Mutex _DataMutex;
        internal Generator(Mutex mutex) 
        {
            _DataMutex = mutex;
        }

        internal void Generate(string[] args, Data data)
        {
            _DataMutex.WaitOne();
            _StringGenerators[args[0]].Invoke(args, data);
            _DataMutex.ReleaseMutex();
        }
        internal void Generate(byte[] args, Data data)
        {     
            char[] objectCode = new char[3];
            for (int i = 0; i < 3; i++)
            {
                objectCode[i] = (char)args[i];
            }
            _DataMutex.WaitOne();
            _ByteGenerators[new string(objectCode)].Invoke(args, data);
            _DataMutex.ReleaseMutex();
        }

        private static void GenerateCrew(byte[] args, Data data)
        {
            UInt16 offset = 0;
            Crew newCrew = new Crew(args, offset);
            data.CrewDictionary.Add(newCrew.Id, newCrew);
            Flight.CrewDictionary.Add(newCrew.Id, newCrew);
        }
        private static void GenerateCrew(string[] args, Data data)
        {
            Crew crew = new Crew(args);
            data.CrewDictionary.Add(crew.Id, crew);
            Flight.CrewDictionary.Add(crew.Id, crew);
        }

        private static void GeneratePassenger(byte[] args, Data data)
        {
            UInt16 offset = 0;
            Passenger newPassenger = new Passenger(args, offset);
            data.PassengerDictionary.Add(newPassenger.Id, newPassenger);
            Flight.LoadDictionary.Add(newPassenger.Id, newPassenger);
        }
        private static void GeneratePassenger(string[] args, Data data)
        {
            Passenger passenger = new Passenger(args);
            data.PassengerDictionary.Add(passenger.Id, passenger);
            Flight.LoadDictionary.Add(passenger.Id, passenger);
        }

        private static void GenerateCargo(byte[] args, Data data)
        {
            Cargo newCargo = new Cargo(args);
            data.CargoDictionary.Add(newCargo.Id, newCargo);
            Flight.LoadDictionary.Add(newCargo.Id, newCargo);
        }
        private static void GenerateCargo(string[] args, Data data)
        {
            Cargo cargo = new Cargo(args);
            data.CargoDictionary.Add(cargo.Id, cargo);
            Flight.LoadDictionary.Add(cargo.Id, cargo);
        }

        private static void GenerateCargoPlane(byte[] args, Data data)
        {
            UInt16 offset = 0;
            CargoPlane newCargoPlane = new CargoPlane(args, offset);
            data.CargoPlaneDictionary.Add(newCargoPlane.Id, newCargoPlane);
            Flight.PlaneDictionary.Add(newCargoPlane.Id, newCargoPlane);
        }
        private static void GenerateCargoPlane(string[] args, Data data)
        {
            CargoPlane cargoPlane = new CargoPlane(args);
            data.CargoPlaneDictionary.Add(cargoPlane.Id, cargoPlane);
            Flight.PlaneDictionary.Add(cargoPlane.Id, cargoPlane);
        }

        private static void GeneratePassengerPlane(byte[] args, Data data)
        {
            UInt16 offset = 0;
            PassengerPlane newPassengerPlane = new PassengerPlane(args, offset);
            data.PassengerPlaneDictionary.Add(newPassengerPlane.Id, newPassengerPlane);
            Flight.PlaneDictionary.Add(newPassengerPlane.Id, newPassengerPlane);
        }
        private static void GeneratePassengerPlane(string[] args, Data data)
        {
            PassengerPlane passengerPlane = new PassengerPlane(args);
            data.PassengerPlaneDictionary.Add(passengerPlane.Id, passengerPlane);
            Flight.PlaneDictionary.Add(passengerPlane.Id, passengerPlane);
        }

        private static void GenerateAirport(byte[] args, Data data)
        {
            Airport newAirport = new Airport(args);
            data.AirportDictionary.Add(newAirport.Id, newAirport);
        }
        private static void GenerateAirport(string[] args, Data data)
        {
            Airport airport = new Airport(args);
            data.AirportDictionary.Add(airport.Id, airport);
        }
               
        private static void GenerateFlight(byte[] args, Data data)
        {
            Flight newFlight = new Flight(args);
            data.FlightDictionary.Add(newFlight.Id, newFlight);
        }
        private static void GenerateFlight(string[] args, Data data)
        {
            Flight flight = new Flight(args);
            data.FlightDictionary.Add(flight.Id, flight);
        }

    }
}
