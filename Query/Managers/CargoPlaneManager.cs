using FlightRadar.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightRadar.Query.Managers
{
    internal class CargoPlaneManager : IManager
    {
        private Dictionary<UInt64, CargoPlane> _CargoPlaneDictionary;

        public CargoPlaneManager(Dictionary<ulong, CargoPlane> cargoPlaneDictionary)
        {
            _CargoPlaneDictionary = cargoPlaneDictionary;
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
