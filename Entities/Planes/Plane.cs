using FlightRadar.Entities.Classes.Media;
using FlightRadar.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightRadar
{
    abstract internal class Plane : BaseOfAll
    {
        public string Serial;
        public string Country; // ISO
        public string Model;

        protected Plane(UInt64 id, string serial, string country, string model) : base(id)
        {
            Serial = serial;
            Country = country;
            Model = model;
        }

        protected Plane(string[] args) : base(args[1])
        {
            Serial = args[2];
            Country = args[3];
            Model = args[4];
        }

        protected Plane(byte[] args, out  UInt16 additionalOffset) : base(args)
        {
            char[] serial = new char[10];
            ReadCharArray(args, 10, 15, serial);
            Serial = new string(serial);

            char[] country = new char[3];
            ReadCharArray(args, 3, 25, country);
            Country = new string(country);

            UInt16 modelLength = BitConverter.ToUInt16(args, 28);
            char[] model = new char[modelLength];
            ReadCharArray(args, modelLength, 30, model);
            Model = new string(model);

            additionalOffset = modelLength;
        }

        
    }
}
