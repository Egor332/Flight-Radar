using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightRadar
{
    internal class Cargo
    {
        public const string Object = "CA";
        public readonly UInt64 Id;
        public Single Weight;
        public string Code;
        public string Description;

        public Cargo(UInt64 id, Single weight, string code, string description)
        {
            Id = id;
            Weight = weight;
            Code = code;
            Description = description;
        }

        public Cargo(string[] args)
        {
            UInt64.TryParse(args[1], out Id);
            Single.TryParse(args[2], out Weight);
            Code = args[3];
            Description = args[4];
        }
    }
}
