using FlightRadar.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightRadar.Query.Managers
{
    internal class CargoManager : IManager
    {
        private static List<string> _FieldsName = new List<string>() { "id", "weight", "code", "description" };

        private Dictionary<UInt64, Cargo> _CargoDictionary;

        private void CheckFields(string[] fieldsName)
        {
            if (fieldsName.Length > _FieldsName.Count) throw new Exceptions.InvalidFieldException();
            foreach (string fieldName in fieldsName)
            {
                if (!_FieldsName.Contains(fieldName)) throw new Exceptions.InvalidFieldException(fieldName);
            }
            
        }

        public CargoManager(Dictionary<ulong, Cargo> cargoDictionary)
        {
            _CargoDictionary = cargoDictionary;
        }

        public void DoDisplay(string[] fieldsName, string whereConditions)
        {
            CheckFields(fieldsName); // will end here if something goes wrong  

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
