using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightRadar.Interfaces
{
    internal interface IManager
    {
        public void DoDisplay(string[] fieldsName, string whereConditions);

        public void DoUpdate(string[] toSet, string whereConditions);

        public void DoDelete(string whereConditions);

        public void DoAdd(string[] toSet);
    }
}
