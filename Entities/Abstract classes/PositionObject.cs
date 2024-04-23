using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightRadar.Entities.Abstract_classes
{
    internal abstract class PositionObject : BaseOfAll
    {
        public Single Longitude;
        public Single Latitude;
        public Single AMSL;

        public PositionObject(UInt64 Id) : base(Id)
        {
            Longitude = 0;
            Latitude = 0;
            AMSL = 0;
        }

        public PositionObject(string id) : base(id)
        {
            Longitude = 0;
            Latitude = 0;
            AMSL = 0;
        }

        public PositionObject(byte[] args) : base(args) 
        {
            Longitude = 0;
            Latitude = 0;
            AMSL = 0;
        }
    }
}
