using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightRadar
{
    internal class Crew : Human
    {
        public const string Object = "C";
        public UInt16 Practice;
        public string Role;

        public Crew(UInt64 id, string name, UInt64 age, string phone, string email, UInt16 practice, string role) : base(id, name, age, phone, email)
        {
            Practice = practice;
            Role = role;
        }

        public Crew(string[] args) : base(args)
        {
            UInt16.TryParse(args[6], out Practice);
            Role = args[7];
        }


    }
}
