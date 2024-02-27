using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightRadar
{
    internal class Generator
    {
        internal Generator() { }

        internal void Generate(string[] args, Data data)
        {
            switch (args[0])
            {
                case "C":
                    Crew crew = new Crew(args);
                    data.CrewList.Add(crew);
                    break;
                case "P":
                    Passenger passenger = new Passenger(args);
                    data.PassengerList.Add(passenger);
                    break;
                case "CA":
                    Cargo cargo = new Cargo(args);
                    data.CargoList.Add(cargo);
                    break;
                case "CP":
                    CargoPlane  cargoPlane = new CargoPlane(args);
                    data.CargoPlaneList.Add(cargoPlane);
                    break;
                case "PP":
                    PassengerPlane passengerPlane = new PassengerPlane(args);
                    data.PassengerPlaneList.Add(passengerPlane);
                    break;
                case "AI":
                    Airport airport = new Airport(args);
                    data.AirportList.Add(airport);
                    break;
                case "FL":
                    Flight flight = new Flight(args);
                    data.FlightList.Add(flight);
                    break;


            }
        }

    }
}
