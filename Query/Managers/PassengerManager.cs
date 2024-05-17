using FlightRadar.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightRadar.Query.Managers
{
    internal class PassengerManager : IManager
    {
        private Dictionary<UInt64, Passenger> _PassengerDictionary;

        public PassengerManager(Dictionary<UInt64, Passenger> passengerDictionary)
        {
            _PassengerDictionary = passengerDictionary;
        }

        public void DoDisplay(string[] fieldsName, string whereConditions)
        {

        }

        public void DoUpdate(string[] toSet, string whereConditions)
        {

        }

        public void DoDelete(string whereConditions)
        {

        }

        public void DoAdd(string[] toSet)
        {

        }
    }
}
