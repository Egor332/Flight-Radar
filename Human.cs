using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightRadar
{
    abstract internal class Human : BaseOfAll
    {
        public string Name;
        public UInt64 Age;
        public string Phone;
        public string Email;

        protected Human(UInt64 id, string name, UInt64 age, string phone, string email) : base(id)
        {
            Name = name;
            Age = age;
            Phone = phone;
            Email = email;
        }

        protected Human(string[] args) : base(args[1])
        {
            Name = args[2];
            UInt64.TryParse(args[3], out Age);
            Phone = args[4];
            Email = args[5];
        }

        protected Human(byte[] args, out UInt16 additionalOffset) : base(args) 
        {
            UInt32 lenght = BitConverter.ToUInt32(args, 3);

            UInt16 nameLength = BitConverter.ToUInt16(args, 15);

            char[] name = new char[nameLength];
            ReadCharArray(args, nameLength, 17, name);
            Name = new string(name); 

            Age = BitConverter.ToUInt16(args, 17 + nameLength);
            char[] phone = new char[12];
            ReadCharArray(args, 12, 19 + nameLength, phone);
            Phone = new string(phone);

            UInt16 emailLength = BitConverter.ToChar(args, 31 + nameLength);
            char[] email = new char[emailLength];
            ReadCharArray(args, emailLength, 33 + nameLength, email);
            Email = new string(email);

            additionalOffset = (UInt16)(emailLength + nameLength);
        }
    }
}
