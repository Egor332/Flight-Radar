using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightRadar
{
    internal class PassengerPlane : Plane
    {
        public const string Object = "PP";
        public UInt16 FirstClassSize;
        public UInt16 BusinessClassSize;
        public UInt16 EconomyClassSize;

        public PassengerPlane(UInt64 id, string serial, string country, string model, UInt16 firstClassSize, UInt16 businessClassSize, UInt16 economyClassSize) : base(id, serial, country, model)
        {
            FirstClassSize = firstClassSize;
            BusinessClassSize = businessClassSize;
            EconomyClassSize = economyClassSize;
        }

        public PassengerPlane(string[] args) : base(args)
        {
            UInt16.TryParse(args[5], out  FirstClassSize);
            UInt16.TryParse(args[6], out BusinessClassSize);
            UInt16.TryParse(args[7], out EconomyClassSize);
        }
    }
}
