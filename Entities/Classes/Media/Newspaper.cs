using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightRadar.Entities.Classes.Media
{
    internal class Newspaper : MediaBase
    {

        public Newspaper(string name) : base(name) { }

        public override string GetDefaultNewsAboutObject(Airport airport)
        {
            return $"{Name} - A report from the {airport.Name} airport, {airport.Country}.";
        }

        public override string GetDefaultNewsAboutObject(CargoPlane plane)
        {
            return $"{Name} - An interview with the crew of {plane.Serial}.";
        }

        public override string GetDefaultNewsAboutObject(PassengerPlane plane)
        {
            return $"{Name} - Breaking news! {plane.Model} aircraft loses EASA fails certification after inspection of {plane.Serial}.";
        }
    }
}
