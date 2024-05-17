using FlightRadar.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightRadar.Query.Managers
{
    internal class FlightManager : IManager
    {
        private Dictionary<UInt64, Flight> _FlightDictionary;

        public FlightManager(Dictionary<ulong, Flight> flightDictionary)
        {
            _FlightDictionary = flightDictionary;
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
