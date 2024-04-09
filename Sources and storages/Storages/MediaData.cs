using ExCSS;
using FlightRadar.Entities.Classes.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightRadar.Sources_and_storages.Storages
{
    internal class MediaData
    {
        public List<MediaBase> ListOfMedia;
        
        public MediaData()
        {
            ListOfMedia = new List<MediaBase> { 
                new Television("Telewizja Abelowa"),
                new Television("Kanal TV-tensor"),
                new Radio("Radio Kwantyfikator"),
                new Radio("Radio Shmem"),
                new Newspaper("Gazeta Kategoryczna"),
                new Newspaper("Dziennik Politechniczny")};
        }

    }
}
