using FlightRadar.Entities.Abstract_classes;
using FlightRadar.Entities.Classes.Media;
using FlightRadar.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightRadar
{
    internal class Airport : PositionObject, IReportable
    {
        public const string Object = "AI";
        public string Name;
        public string Code;
        public string Country; // ISO

        public Airport(UInt64 id, string name, string code, Single longitude, Single latitude, Single AMSL, string country) : base(id)
        {
            Name = name;
            Code = code;
            Longitude = longitude;
            Latitude = latitude;
            AMSL = AMSL;
            Country = country;
        }

        public Airport(string[] args) : base(args[1])
        {
            Name = args[2];
            Code = args[3];
            Single.TryParse(args[4], out Longitude);
            Single.TryParse(args[5], out Latitude);
            Single.TryParse(args[6], out AMSL);
            Country = args[7];
        }

        public Airport(byte[] args) : base(args)
        {
            UInt16 nameLenght = BitConverter.ToUInt16(args, 15);
            char[] name = new char[nameLenght];
            ReadCharArray(args, nameLenght, 17, name);
            Name = new string(name);

            char[] code = new char[3];
            ReadCharArray(args, 3, 17 + nameLenght, code);
            Code = new string(code);

            Longitude = BitConverter.ToSingle(args, 20 + nameLenght);

            Latitude = BitConverter.ToSingle(args, 24  + nameLenght);

            AMSL = BitConverter.ToSingle(args, 28 + nameLenght);

            char[] country = new char[3];
            ReadCharArray(args, 3, 32 + nameLenght, country);
            Country = new string(country);
        }

        public string Report(MediaBase media)
        {
            return media.GetDefaultNewsAboutObject(this);
        }
    }
}
