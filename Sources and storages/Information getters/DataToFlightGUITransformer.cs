using Avalonia.Controls;
using BruTile.Predefined;
using Mapsui.Projections;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightRadar.Sources_and_storages
{
    internal class DataToFlightGUITransformer
    {

        private List<Flight> _FlightsList;

        private FlightsGUIData _FlightsData;

        private Dictionary<UInt64, Airport> _AirportDictionary;

        public DataToFlightGUITransformer(List<Flight> flightsList, FlightsGUIData flightsData, Dictionary<UInt64, Airport> airportsDictionary)
        {
            _FlightsList = flightsList;
            _FlightsData = flightsData;
            _AirportDictionary = airportsDictionary;
            
        }

        public void DataToGUIDataFirstTime()
        {
            List<FlightGUI> flightsGUI = new List<FlightGUI>();

            foreach (Flight flight in _FlightsList)
            {
                Single precentage = FindPrecentageOfFlight(flight.TakeOffTime, flight.LandingTime);

                Single lon;
                Single lat;

                (lon, lat) = GetCurrentLonLat(flight, precentage);

                flight.Latitude = lat;
                flight.Longtitude = lon;

                FlightGUI newFlightGUI = new FlightGUI
                {
                    ID = flight.Id,
                    WorldPosition = new WorldPosition(lat, lon),
                    MapCoordRotation = 0.63
                };
                flightsGUI.Add(newFlightGUI);
            }
            _FlightsData.UpdateFlights(flightsGUI);
        }

        public void DataToGUIData()
        {
            List<FlightGUI> flightsGUI = new List<FlightGUI>();

            foreach (Flight flight in _FlightsList)
            {
                Single percentage = FindPrecentageOfFlight(flight.TakeOffTime, flight.LandingTime);
                if (percentage <= 0 || percentage >= 1)
                {
                    continue;
                }

                Single lon;
                Single lat;
                (lon, lat) = GetCurrentLonLat(flight, percentage);

                double angle = GetAngle(lon, lat, flight.Longtitude, flight.Latitude);

                FlightGUI newFlightGUI = new FlightGUI
                {
                    ID = flight.Id,
                    WorldPosition = new WorldPosition(lat, lon),
                    MapCoordRotation = angle
                };
                flightsGUI.Add(newFlightGUI);
            }
            _FlightsData.UpdateFlights(flightsGUI);
        }
        
        private Single FindPrecentageOfFlight(string TakeOffTime, string LandingTime)
        {
            TimeOnly startTime;
            TimeOnly endTime;

            if ((!TimeOnly.TryParse(TakeOffTime, out startTime)) || (!TimeOnly.TryParse(LandingTime, out endTime)))
            {
                Console.WriteLine("Time parse error");
                return -1;
            }

            TimeOnly currentTime = TimeOnly.FromDateTime(DateTime.Now);

            if (!currentTime.IsBetween(startTime, endTime))
            {
                return -1;
            }

            TimeSpan wholeInterval =  endTime - startTime;
            TimeSpan currentInterval = currentTime - startTime;

            Single percentage = (Single)(currentInterval.TotalMilliseconds / wholeInterval.TotalMilliseconds);

            return percentage;
        }

        private (Single, Single) GetCurrentLonLat(Flight flight, Single precentage)
        {
            Single lon;
            Single lat;
            if (precentage > (Single)1)
            {
                lon = _AirportDictionary[flight.TargetId].Longitude;
                lat = _AirportDictionary[flight.TargetId].Latitude;
            }
            else if (precentage < (Single)0)
            {
                lon = _AirportDictionary[flight.OriginId].Longitude;
                lat = _AirportDictionary[flight.OriginId].Latitude;
            }
            else
            {
                lon = GetCurrentPosition(_AirportDictionary[flight.OriginId].Longitude, _AirportDictionary[flight.TargetId].Longitude, precentage);
                lat = GetCurrentPosition(_AirportDictionary[flight.OriginId].Latitude, _AirportDictionary[flight.TargetId].Latitude, precentage);
            }
            return (lon, lat);
        }

        private Single GetCurrentPosition(Single start, Single end, Single precentage)
        {
            return start + ((end - start) * precentage);
        }

        private double GetAngle(Single lon, Single lat, Single lonPrev, Single latPrev)
        {
            double x, y, xPrev, yPrev;
            (x, y) = SphericalMercator.FromLonLat(lon, lat);
            (xPrev, yPrev) = SphericalMercator.FromLonLat(lonPrev, latPrev);
            double dy = y - yPrev;
            double dx = x - xPrev;

            return Math.Atan2(dx, dy);
        }

    }
}
