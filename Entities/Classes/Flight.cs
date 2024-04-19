using FlightRadar.Entities.Abstract_classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightRadar
{
    internal class Flight : BaseOfAll
    {
        public static Dictionary<UInt64, Plane> PlaneDictionary = new Dictionary<UInt64, Plane>();
        public static Dictionary<UInt64, Crew> CrewDictionary = new Dictionary<UInt64, Crew>();
        public static Dictionary<UInt64, ILoadable> LoadDictionary = new Dictionary<UInt64, ILoadable>();

        public const string Object = "FL";
        public UInt64 OriginId;
        public UInt64 TargetId;
        public string TakeOffTime;
        public string LandingTime;
        public Single Longtitude;
        public Single Latitude;
        public Single? ASML;
        public UInt64 PlaneId;
        public UInt64[] CrewId;
        public UInt64[] LoadId; //  could be list of passengers id or list of carrgo id 

        public Flight(UInt64 id, UInt64 originId, UInt64 targetId, string takeOfTime, string landingTime, Single longtitude, Single latitude, Single asml, UInt64 planeId, UInt64[] crewId, UInt64[] loadId) : base(id)
        {
            OriginId = originId;
            TargetId = targetId;
            TakeOffTime = takeOfTime;
            LandingTime = landingTime;
            Longtitude = longtitude;
            Latitude = latitude;
            ASML = asml;
            PlaneId = planeId;
            CrewId = crewId;
            LoadId = loadId;
        }

        public Flight(string[] args) : base(args[1])
        {
            UInt64.TryParse(args[2], out  OriginId);
            UInt64.TryParse(args[3], out TargetId);
            TakeOffTime = args[4];
            LandingTime = args[5];

            Single longt, lat, asml; // strange error in other case
            Single.TryParse(args[6], out longt);
            Single.TryParse(args[7], out lat);
            Single.TryParse(args[8], out asml);
            Longtitude = longt;
            Latitude = lat;
            ASML = asml;

            UInt64.TryParse(args[9], out PlaneId);
            StringArrayToArrayOfUInt64(args[10], out CrewId);
            StringArrayToArrayOfUInt64(args[11], out LoadId);            
        }

        public Flight(byte[] args) : base(args)
        {
            OriginId = BitConverter.ToUInt64(args, 15);

            TargetId = BitConverter.ToUInt64(args, 23);

            UInt64 takeOffTimeMsAfterEpoch = BitConverter.ToUInt64(args, 31);
            TakeOffTime = MsAfterEpochToString(takeOffTimeMsAfterEpoch);

            UInt64 landingTimeMsAfterEpoch = BitConverter.ToUInt64(args, 39);
            LandingTime = MsAfterEpochToString(landingTimeMsAfterEpoch);

            PlaneId = BitConverter.ToUInt64(args, 47);

            UInt16 crewCount = BitConverter.ToUInt16(args, 55);
            CrewId = new UInt64[crewCount];
            BytesToArrayOfUInt64(args, crewCount, 57, CrewId);

            UInt16 loadCount = BitConverter.ToUInt16(args, 57 + 8*crewCount);
            LoadId = new UInt64[loadCount];
            BytesToArrayOfUInt64(args, loadCount, 59 + 8*crewCount, LoadId);

           
            ASML = null;
            Longtitude = 0; // that will be changed after first try to dispaly planes (DataToFlightGUITransformer.DataToGUIDataFirstTime)
            Latitude = 0; // that will be changed after first try to dispaly planes (DataToFlightGUITransformer.DataToGUIDataFirstTime)
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

        private string MsAfterEpochToString(UInt64 msAfterEpoch)
        {
            DateTime epochTime = DateTime.UnixEpoch;
            DateTime dateTime = epochTime.AddMilliseconds(msAfterEpoch);
            return dateTime.ToString();
        }

        private void BytesToArrayOfUInt64(byte[] source, UInt16 length, int offset, UInt64[] target)
        {
            for (int i = 0; i < length; i++)
            {
                target[i] = BitConverter.ToUInt64(source, offset + 8 * i);
            }
        }
    }
}
