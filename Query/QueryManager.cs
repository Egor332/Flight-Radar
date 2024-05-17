using FlightRadar.Interfaces;
using FlightRadar.Query.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightRadar.Query
{
    internal class QueryManager
    {
        private Data _Data;
        private Dictionary<string, IManager> _Managers;

        public QueryManager(Data data) 
        {
            _Data = data;
            _Managers = new Dictionary<string, IManager>()
            {
                { "airport", new AirportManager(data.AirportDictionary) },
                { "cargo", new CargoManager(data.CargoDictionary) },
                { "crew", new CrewManager(data.CrewDictionary) },
                { "Flight", new FlightManager(data.FlightDictionary) },
                { "passengerplane", new PassengerPlaneManager(data.PassengerPlaneDictionary) },
                { "cargoplane", new CargoPlaneManager(data.CargoPlaneDictionary) },
                { "passenger", new PassengerManager(data.PassengerDictionary) }
            };
        }

        public void DoDisplay(string from, string[] fieldsName, string whereConditions)
        {
            try { _Managers[from].DoDisplay(fieldsName, whereConditions); } catch { }
        }

        public void DoUpdate(string from, string[] toSet, string whereConditions)
        {
            try { _Managers[from].DoUpdate(toSet, whereConditions); } catch { }

        }

        public void DoDelete(string from, string whereConditions)
        {
            try { _Managers[from].DoDelete(whereConditions); } catch { }
        }

        public void DoAdd(string from, string[] toSet)
        {

            try { _Managers[from].DoAdd(toSet); } catch { }
        }

    }
}
