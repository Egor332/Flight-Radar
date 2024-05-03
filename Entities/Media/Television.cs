using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightRadar.Entities.Classes.Media
{
    internal class Television : MediaBase
    {

        public Television(string name) : base(name) { }
        public override string GetDefaultNewsAboutObject(Airport airport)
        {
            return $"<An image of {airport.Name} airport";
        }

        public override string GetDefaultNewsAboutObject(CargoPlane plane)
        {
            return $"<An image of {plane.Serial} cargo plane>";
        }

        public override string GetDefaultNewsAboutObject(PassengerPlane plane)
        {
            return $"<An image of {plane.Serial} passenger plane>";
        }
    }
}
