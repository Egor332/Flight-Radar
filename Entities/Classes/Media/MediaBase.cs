using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightRadar.Entities.Classes.Media
{
    internal abstract class MediaBase
    {
        public string Name;

        public MediaBase(string name)
        {
            Name = name;
        }

        public abstract string GetDefaultNewsAboutObject(Airport airport);

        public abstract string GetDefaultNewsAboutObject(CargoPlane plane);

        public abstract string GetDefaultNewsAboutObject(PassengerPlane plane);
    }
}
