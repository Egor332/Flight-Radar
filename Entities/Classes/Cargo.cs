using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightRadar
{
    internal class Cargo : BaseOfAll
    {
        public const string Object = "CA";
        public Single Weight;
        public string Code;
        public string Description;

        public Cargo(UInt64 id, Single weight, string code, string description) : base(id)
        {
            Weight = weight;
            Code = code;
            Description = description;
        }

        public Cargo(string[] args) : base(args[1])
        {
            Single.TryParse(args[2], out Weight);
            Code = args[3];
            Description = args[4];
        }

        public Cargo(byte[] args) : base(args) 
        {
            Weight = BitConverter.ToSingle(args, 15);

            char[] code = new char[6];
            ReadCharArray(args, 6, 19, code);
            Code = new string(code);

            UInt16 descLength = BitConverter.ToUInt16(args, 25);
            char[] description = new char[descLength];
            ReadCharArray(args, descLength, 27, description);
            Description = new string(description);
        }
    }
}
