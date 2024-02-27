using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightRadar
{
    abstract internal class Human
    {
        public readonly UInt64 Id;
        public string Name;
        public UInt64 Age;
        public string Phone;
        public string Email;

        protected Human(UInt64 id, string name, UInt64 age, string phone, string email)
        {
            Id = id;
            Name = name;
            Age = age;
            Phone = phone;
            Email = email;
        }

        protected Human(string[] args)
        {

            UInt64.TryParse(args[1], out Id);
            Name = args[2];
            UInt64.TryParse(args[3], out Age);
            Phone = args[4];
            Email = args[5];
        }
    }
}
