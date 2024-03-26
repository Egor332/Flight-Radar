using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightRadar
{
    internal class CargoPlane : Plane
    {
        public const string Object = "CP";
        public Single MaxLoad;

        public CargoPlane(UInt64 id, string serial, string country, string model, Single maxLoad) : base(id, serial, country, model)
        {
            MaxLoad = maxLoad;
        }

        public CargoPlane(string[] args) : base(args)
        {
            Single.TryParse(args[5], out MaxLoad);
        }

        public CargoPlane(byte[] args, UInt16 additionalOffset) : base(args, out additionalOffset)
        {
            MaxLoad = BitConverter.ToSingle(args, 30 + additionalOffset);
        }
    }
}
