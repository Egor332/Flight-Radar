using FlightRadar.Entities.Abstract_classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightRadar
{
    internal class Flight : PositionObject
    {
        public static Dictionary<UInt64, BaseOfAll> PlaneDictionary = new Dictionary<UInt64, BaseOfAll>();
        public static Dictionary<UInt64, BaseOfAll> CrewDictionary = new Dictionary<UInt64, BaseOfAll>();
        public static Dictionary<UInt64, BaseOfAll> LoadDictionary = new Dictionary<UInt64, BaseOfAll>();

        public const string Object = "FL";
        public UInt64 OriginId;
        public UInt64 TargetId;
        public string TakeOffTime;
        public string LandingTime;
        public BaseOfAll PlaneRef;
        public BaseOfAll[] CrewRef;
        public BaseOfAll[] LoadRef;

        private Single startLat = 0;
        private Single startLon = 0;
        private TimeOnly startTime;

        public Flight(UInt64 id, UInt64 originId, UInt64 targetId, string takeOfTime, string landingTime, Single longtitude, Single latitude, Single asml, Plane plane, Crew[] crew, BaseOfAll[] load) : base(id)
        {
            OriginId = originId;
            TargetId = targetId;
            TakeOffTime = takeOfTime;
            LandingTime = landingTime;
            Longitude = longtitude;
            Latitude = latitude;
            AMSL = asml;
            PlaneRef = plane;
            CrewRef = crew;
            LoadRef = load;

            TimeOnly.TryParse(takeOfTime, out startTime);
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
            Longitude = longt;
            Latitude = lat;
            AMSL = asml;

            UInt64 planeId;
            UInt64.TryParse(args[9], out planeId);
            PlaneRef = PlaneDictionary[planeId];

            StringArrayToArrayOfCrew(args[10], out CrewRef);
            StringArrayToArrayOfLoad(args[11], out LoadRef);
            TimeOnly.TryParse(TakeOffTime, out startTime);
        }

        public Flight(byte[] args) : base(args)
        {
            OriginId = BitConverter.ToUInt64(args, 15);

            TargetId = BitConverter.ToUInt64(args, 23);

            UInt64 takeOffTimeMsAfterEpoch = BitConverter.ToUInt64(args, 31);
            TakeOffTime = MsAfterEpochToString(takeOffTimeMsAfterEpoch);

            UInt64 landingTimeMsAfterEpoch = BitConverter.ToUInt64(args, 39);
            LandingTime = MsAfterEpochToString(landingTimeMsAfterEpoch);

            UInt64 planeId = BitConverter.ToUInt64(args, 47);
            PlaneRef = PlaneDictionary[planeId];

            UInt16 crewCount = BitConverter.ToUInt16(args, 55);
            CrewRef = new Crew[crewCount];
            BytesToArrayOfCrew(args, crewCount, 57, CrewRef);

            UInt16 loadCount = BitConverter.ToUInt16(args, 57 + 8*crewCount);
            LoadRef = new BaseOfAll[loadCount];
            BytesToArrayOfLoad(args, loadCount, 59 + 8*crewCount, LoadRef);

            TimeOnly.TryParse(TakeOffTime, out startTime);

            AMSL = 0;
            Longitude = 0; // that will be changed after first try to dispaly planes (DataToFlightGUITransformer.DataToGUIDataFirstTime)
            Latitude = 0; // that will be changed after first try to dispaly planes (DataToFlightGUITransformer.DataToGUIDataFirstTime)
        }

        public void SetStartArgs(Single lat, Single lon, TimeOnly time)
        {
            startTime = time;
            startLat = lat;
            startLon = lon;
        }

        /*Function transform string "[*;*;*;...;*]" to the array of UInt64
         values in string MUST BE SEPARATED BY ;*/
        private void StringArrayToArrayOfCrew(string source, out BaseOfAll[] target)
        {
            string[] valuesStr = source.Substring(1, source.Length - 2).Split(";");
            target = new Crew[valuesStr.Length];
            UInt64 crewId;
            for (int i = 0; i < valuesStr.Length; i++)
            {
                UInt64.TryParse(valuesStr[i], out crewId);
                target[i] = CrewDictionary[crewId];
            }

        }

        private void StringArrayToArrayOfLoad(string source, out BaseOfAll[] target)
        {
            string[] valuesStr = source.Substring(1, source.Length - 2).Split(";");
            target = new BaseOfAll[valuesStr.Length];
            UInt64 loadId;
            for (int i = 0; i < valuesStr.Length; i++)
            {
                
                UInt64.TryParse(valuesStr[i], out loadId);
                target[i] = LoadDictionary[loadId];
            }

        }


        private string MsAfterEpochToString(UInt64 msAfterEpoch)
        {
            DateTime epochTime = DateTime.UnixEpoch;
            DateTime dateTime = epochTime.AddMilliseconds(msAfterEpoch);
            return dateTime.ToString();
        }

        private void BytesToArrayOfCrew(byte[] source, UInt16 length, int offset, BaseOfAll[] target)
        {
            UInt64 id;
            for (int i = 0; i < length; i++)
            {
                id = BitConverter.ToUInt64(source, offset + 8 * i);
                target[i] = CrewDictionary[id];
            }
        }

        private void BytesToArrayOfLoad(byte[] source, UInt16 length, int offset, BaseOfAll[] target)
        {
            UInt64 id;
            for (int i = 0; i < length; i++)
            {
                id = BitConverter.ToUInt64(source, offset + 8 * i);
                target[i] = LoadDictionary[id];
            }
        }
    }
}
