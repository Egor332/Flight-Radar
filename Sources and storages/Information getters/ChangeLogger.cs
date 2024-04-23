using NetworkSourceSimulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightRadar.Sources_and_storages.Information_getters
{
    internal class ChangeLogger
    {

        private StreamWriter _writer;

        public ChangeLogger(StreamWriter writer)
        {
            _writer = writer;
            _writer.WriteLine("--- Logger opened ---");
            _writer.Flush();
        }
        public void LogWrongId(UInt64 Id)
        {
            string logString = string.Format("{0, 6}", DateTime.Now.ToString("HH:mm:ss")) + ":" +
                string.Format("{0, -22}", "Wrong object ID:") + string.Format("{0, -8}", Id.ToString());

            _writer.WriteLine(logString);
            _writer.Flush();
        }

        public void LogBusyId(UInt64 Id)
        {
            string logString = string.Format("{0, 6}", DateTime.Now.ToString("HH:mm:ss")) + ":" +
                string.Format("{0, -22}", "Busy object ID:") + string.Format("{0, -8}", Id.ToString());
            _writer.WriteLine(logString);
            _writer.Flush();
        }

        public void LogIdChange(IDUpdateArgs e)
        {
            string logString = string.Format("{0, 6}", DateTime.Now.ToString("HH:mm:ss")) + ":" +
                string.Format("{0, -22}", "Change object ID:") + string.Format("{0, -8}", "from:") +
                string.Format("{0, -8}", e.ObjectID.ToString()) + string.Format("{0, -5}", "to:") +
                string.Format("{0, -8}", e.NewObjectID.ToString());
            _writer.WriteLine(logString);
            _writer.Flush();
        }

        public void LogContactUpdateChange(ContactInfoUpdateArgs e)
        {
            string logString = string.Format("{0, 6}", DateTime.Now.ToString("HH:mm:ss")) + ":" +
                string.Format("{0, -22}", "Change contacts, object ID:") + string.Format("{0, -8}", e.ObjectID.ToString()) +
                string.Format("{0, -14}", "New phone:") + string.Format("{0, -14}", e.PhoneNumber) +
                string.Format("{0, -14}", "New email:") + string.Format("{0, -25}", e.EmailAddress);
            _writer.WriteLine(logString);
            _writer.Flush();
        }

        public void LogPositionChange(PositionUpdateArgs e)
        {
            string logString = string.Format("{0, 6}", DateTime.Now.ToString("HH:mm:ss")) + ":" +
                string.Format("{0, -22}", "Change positon, object ID:") + string.Format("{0, -8}", e.ObjectID.ToString()) +
                string.Format("{0, -14}", "Longitude:") + string.Format("{0, -10}", e.Longitude) +
                string.Format("{0, -14}", "Latitude:") + string.Format("{0, -10}", e.Latitude) +
                string.Format("{0, -10}", "AMSL:") + string.Format("{0, -10}", e.AMSL);
            _writer.WriteLine(logString);
            _writer.Flush();
        }

        public void Close()
        {
            _writer.WriteLine("--- Logger closed ---");
            _writer.Flush();
            _writer.Close();
        }
    }
}
