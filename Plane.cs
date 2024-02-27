using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightRadar
{
    abstract internal class Plane
    {
        public readonly UInt64 Id;
        public string Serial;
        public string Country; // ISO
        public string Model;

        protected Plane(UInt64 id, string serial, string country, string model)
        {
            Id = id;
            Serial = serial;
            Country = country;
            Model = model;
        }

        protected Plane(string[] args)
        {
            UInt64.TryParse(args[1], out Id);
            Serial = args[2];
            Country = args[3];
            Model = args[4];
        }
    }
}
