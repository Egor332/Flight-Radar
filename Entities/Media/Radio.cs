using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightRadar.Entities.Classes.Media
{
    internal class Radio : MediaBase
    {
        public Radio(string name) : base(name) { }

        public override string GetDefaultNewsAboutObject(Airport airport)
        {
            return $"Reporting for {Name}, Ladies and Gentelmen, we are at the {airport.Name} airport.";
        }

        public override string GetDefaultNewsAboutObject(CargoPlane plane)
        {
            return $"Reporting for {Name}, Ladies and Gentelmen, we are seeing the {plane.Serial} aircraft fly above us.";
        }

        public override string GetDefaultNewsAboutObject(PassengerPlane plane)
        {
            return $"Reporting for {Name}, Ladies and Gentelmen, we are just witnessed {plane.Serial} take off.";
        }
    }
}
