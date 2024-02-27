using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightRadar
{
    internal class Flight
    {
        public const string Object = "FL";
        public readonly UInt64 Id;
        public UInt64 OriginId;
        public UInt64 TargetId;
        public string TakeOfTime;
        public string LandingTime;
        public Single Longtitude;
        public Single Latitude;
        public Single ASML;
        public UInt64 PlaneId;
        public UInt64[] CrewId;
        public UInt64[] LoadId; //  could be list of passengers id or list of carrgo id 

        public Flight(UInt64 id, UInt64 originId, UInt64 targetId, string takeOfTime, string landingTime, Single longtitude, Single latitude, Single ASML, UInt64 planeId, UInt64[] crewId, UInt64[] loadId)
        {
            Id = id;
            OriginId = originId;
            TargetId = targetId;
            TakeOfTime = takeOfTime;
            LandingTime = landingTime;
            Longtitude = longtitude;
            Latitude = latitude;
            ASML = ASML;
            PlaneId = planeId;
            CrewId = crewId;
            LoadId = loadId;
        }

        public Flight(string[] args)
        {
            UInt64.TryParse(args[1], out Id);
            UInt64.TryParse(args[2], out  OriginId);
            UInt64.TryParse(args[3], out TargetId);
            TakeOfTime = args[4];
            LandingTime = args[5];
            Single.TryParse(args[6], out Longtitude);
            Single.TryParse(args[7], out Latitude);
            Single.TryParse(args[8], out ASML);
            UInt64.TryParse(args[9], out PlaneId);
            StringArrayToArrayOfUInt64(args[10], out CrewId);
            StringArrayToArrayOfUInt64(args[11], out LoadId);            
        }

        /*Function transform string "[*;*;*;...;*]" to the array of UInt64
         values in string MUST BE SEPARATED BY ;*/
        private void StringArrayToArrayOfUInt64(string source, out UInt64[] target)
        {
            string[] valuesStr = source.Substring(1, source.Length - 2).Split(";");
            target = new UInt64[valuesStr.Length];
            for (int i = 0; i < valuesStr.Length; i++)
            {
                UInt64.TryParse(valuesStr[i], out target[i]);
            }

        }
    }
}
