using FlightRadar.Entities.Classes.Media;
using FlightRadar.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightRadar.Sources_and_storages
{
    internal class NewsGenerator
    {
        private List<IReportable> _ObjectsToReport;
        private List<MediaBase> _MediaList;
        private List<(IReportable obj, MediaBase media)> _AllPairs;

        public NewsGenerator(List<IReportable> objectsToReport, List<MediaBase> mediaList)
        {
            _ObjectsToReport = objectsToReport;
            _MediaList = mediaList;
            _AllPairs = CreateCartesianProduct(objectsToReport, mediaList);
        }

        private List<(IReportable, MediaBase)> CreateCartesianProduct(List<IReportable> objectsToReport, List<MediaBase> mediaList)
        {
            List<(IReportable obj, MediaBase media)> pairs = new List<(IReportable, MediaBase)>();
            foreach (IReportable obj in objectsToReport)
            {
                foreach (MediaBase media in mediaList)
                {
                    pairs.Add((obj, media));
                }
            }
            return pairs;
        }

        public string? GenerateNextNews()
        {
            if (_AllPairs.Count == 0) 
            { 
                return null; 
            }

            string reportString = _AllPairs[0].obj.Report(_AllPairs[0].media);
            _AllPairs.RemoveAt(0);
            return reportString;
        }

    }
}
