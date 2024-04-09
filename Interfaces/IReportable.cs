using FlightRadar.Entities.Classes.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightRadar.Interfaces
{
    internal interface IReportable
    {
        string Report(MediaBase media);
    }
}
