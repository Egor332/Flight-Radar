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
        private Mutex _DataMutex;
        internal Generator(Mutex mutex) 
        {
            _DataMutex = mutex;
        }

        internal void Generate(string[] args, Data data)
        {
            _DataMutex.WaitOne();
            switch (args[0])
            {
                case "C":
                    GenerateCrew(args, data);
                    break;
                case "P":
                    GeneratePassenger(args, data);
                    break;
                case "CA":
                    GenerateCargo(args, data);
                    break;
                case "CP":
                    GenerateCargoPlane(args, data);
                    break;
                case "PP":
                    GeneratePassengerPlane(args, data);
                    break;
                case "AI":
                    GenerateAirport(args, data);
                    break;
                case "FL":
                    GenerateFlight(args, data); 
                    break;
            }
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
            switch (new string(objectCode))
            {
                case "NCR":
                    GenerateCrew(args, data);
                    break;
                case "NPA":
                    GeneratePassenger(args, data);
                    break;
                case "NCA":
                    GenerateCargo(args, data);
                    break;
                case "NCP":
                    GenerateCargoPlane(args, data);
                    break;
                case "NPP":
                    GeneratePassengerPlane(args, data);
                    break;
                case "NAI":
                    GenerateAirport(args, data);
                    break;
                case "NFL":
                    GenerateFlight(args, data);
                    break;
            }
            _DataMutex.ReleaseMutex();
        }

        private void GenerateCrew(byte[] args, Data data)
        {
            UInt16 offset = 0;
            Crew newCrew = new Crew(args, offset);
            data.CrewList.Add(newCrew);
        }
        private void GenerateCrew(string[] args, Data data)
        {
            Crew crew = new Crew(args);
            data.CrewList.Add(crew);
        }

        private void GeneratePassenger(byte[] args, Data data)
        {
            UInt16 offset = 0;
            Passenger newPassenger = new Passenger(args, offset);
            data.PassengerList.Add(newPassenger);
        }
        private void GeneratePassenger(string[] args, Data data)
        {
            Passenger passenger = new Passenger(args);
            data.PassengerList.Add(passenger);
        }

        private void GenerateCargo(byte[] args, Data data)
        {
            Cargo newCargo = new Cargo(args);
            data.CargoList.Add(newCargo);
        }
        private void GenerateCargo(string[] args, Data data)
        {
            Cargo cargo = new Cargo(args);
            data.CargoList.Add(cargo);
        }

        private void GenerateCargoPlane(byte[] args, Data data)
        {
            UInt16 offset = 0;
            CargoPlane newCargoPlane = new CargoPlane(args, offset);
            data.CargoPlaneList.Add(newCargoPlane);
        }
        private void GenerateCargoPlane(string[] args, Data data)
        {
            CargoPlane cargoPlane = new CargoPlane(args);
            data.CargoPlaneList.Add(cargoPlane);
        }

        private void GeneratePassengerPlane(byte[] args, Data data)
        {
            UInt16 offset = 0;
            PassengerPlane newPassengerPlane = new PassengerPlane(args, offset);
            data.PassengerPlaneList.Add(newPassengerPlane);
        }
        private void GeneratePassengerPlane(string[] args, Data data)
        {
            PassengerPlane passengerPlane = new PassengerPlane(args);
            data.PassengerPlaneList.Add(passengerPlane);
        }

        private void GenerateAirport(byte[] args, Data data)
        {
            Airport newAirport = new Airport(args);
            data.AirportList.Add(newAirport);
        }
        private void GenerateAirport(string[] args, Data data)
        {
            Airport airport = new Airport(args);
            data.AirportList.Add(airport);
        }
               
        private void GenerateFlight(byte[] args, Data data)
        {
            Flight newFlight = new Flight(args);
            data.FlightList.Add(newFlight);
        }
        private void GenerateFlight(string[] args, Data data)
        {
            Flight flight = new Flight(args);
            data.FlightList.Add(flight);
        }

    }
}
