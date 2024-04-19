using FlightRadar.Entities.Abstract_classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightRadar
{
    internal class Passenger : Human, ILoadable
    {
        public const string Object = "P";
        public string Class;
        public UInt64 Miles;

        public Passenger(UInt64 id, string name, UInt64 age, string phone, string email, string cl, UInt64  miles) : base(id, name, age, phone, email) 
        {
            
            Class = cl;
            Miles = miles;
        }

        public Passenger(string[] args) : base(args)
        {
            Class = args[6];
            UInt64.TryParse(args[7], out Miles);            
        }

        public Passenger(byte[] args, UInt16 additionalOffset) : base(args, out additionalOffset)
        {
            Class = Encoding.ASCII.GetString(args, 33 + additionalOffset, 1);
            Miles = BitConverter.ToUInt64(args, 34 + additionalOffset);
        }
        
       
    }
}
