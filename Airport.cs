using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightRadar
{
    internal class Airport
    {
        public const string Object = "AI";
        public readonly UInt64 Id;
        public string Name;
        public string Code;
        public Single Longitude;
        public Single Latitude;
        public Single AMSL;
        public string Country; // ISO

        public Airport(UInt64 id, string name, string code, Single longitude, Single latitude, Single AMSL, string country)
        {
            Id = id;
            Name = name;
            Code = code;
            Longitude = longitude;
            Latitude = latitude;
            AMSL = AMSL;
            Country = country;
        }

        public Airport(string[] args)
        {
            UInt64.TryParse(args[1], out Id);
            Name = args[2];
            Code = args[3];
            Single.TryParse(args[4], out Longitude);
            Single.TryParse(args[5], out Latitude);
            Single.TryParse(args[6], out AMSL);
            Country = args[7];
        }
    }
}
